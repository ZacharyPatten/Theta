using System;

using Theta;
using Theta.Mathematics;
using Theta.Structures;

namespace Validation
{
	// This is jsut a crappy little playground for me to test stuff

	class Program
	{
		static void Main(string[] args)
		{
            // Data Set Generation
            Random random = new Random(7);
            List<Vector<double>> vectors = new ListArray<Vector<double>>();
            int test_size = 100000;
            for (int i = 0; i < test_size; i++)
                vectors.Add(new Vector<double>(i - (test_size / 2), random.Next(10000) - (test_size / 2) + i));

            // Simple Linear Regression
            double slope, y_intercept;
            Compute<double>.LinearRegression2D(vectors.Stepper(), out slope, out y_intercept);
            Console.WriteLine("Slope: " + slope);
            Console.WriteLine("Y-Intercept: " + y_intercept);

            Console.WriteLine(Compute<float>.Pi);
            Console.WriteLine(Compute<double>.Pi);
            Console.WriteLine(Compute<Fraction64>.Pi);
            Console.WriteLine(Compute<Fraction128>.Pi);


            TestOmnitree1();
            Console.WriteLine();
            TestOmnitree2();
            Console.WriteLine();

            Console.WriteLine("Press Enter To Exit...");
			Console.ReadLine();
		}

        public class Object3D
        {
            public int Id { get; set; }
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }

            public Object3D(int id, double x, double y, double z)
            {
                this.Id = id;
                this.X = x;
                this.Y = y;
                this.Z = z;
            }
        }

		public static void TestMath()
		{
			#region math stuffs

			Console.WriteLine("Negate:         " + (Compute<int>.Negate(7) == -7));
			Console.WriteLine("Add:            " + (Compute<int>.Add(7, 7) == 14));
			Console.WriteLine("Subtract:       " + (Compute<int>.Subtract(14, 7) == 7));
			Console.WriteLine("Multiply:       " + (Compute<int>.Multiply(7, 7) == 49));
			Console.WriteLine("Divide:         " + (Compute<int>.Divide(14, 7) == 2));
			Console.WriteLine("AbsoluteValue:  " + (Compute<int>.AbsoluteValue(7) == 7 && Compute<int>.AbsoluteValue(-7) == 7));
			Console.WriteLine("Clamp:          " + (Compute<int>.Clamp(7, 6, 8) == 7));
			Console.WriteLine("Maximum:        " + (Compute<int>.Maximum((Step<int> step) => { step(1); step(2); step(3); }) == 3));
			Console.WriteLine("Minimum:        " + (Compute<int>.Minimum((Step<int> step) => { step(1); step(2); step(3); }) == 1));
			Console.WriteLine("LessThan:       " + (Compute<int>.LessThan(1, 2) == true && Compute<int>.LessThan(2, 1) == false));
			Console.WriteLine("GreaterThan:    " + (Compute<int>.GreaterThan(2, 1) == true && Compute<int>.GreaterThan(1, 2) == false));
			Console.WriteLine("Compare:        " + (Compute<int>.Compare(2, 1) == Comparison.Greater && Compute<int>.Compare(1, 2) == Comparison.Less && Compute<int>.Compare(1, 1) == Comparison.Equal));
			Console.WriteLine("Equate:         " + (Compute<int>.Equate(2, 1) == false && Compute<int>.Equate(1, 1) == true));
            Console.WriteLine("EqualsLeniency: " + (Compute<int>.Equals(2, 1, 1) == true && Compute<int>.Equals(2, 1, 0) == false && Compute<int>.Equals(1, 1, 0) == true));

			#endregion
		}

		public static void TestOmnitree1()
		{
			#region construction

            Omnitree.Location<Object3D, double, double, double> locate = (Object3D record, out double a, out double b, out double c) =>
			{
                a = record.X;
                b = record.Y;
                c = record.Z;
			};

			Compute<double>.Compare(0, 0);
            OmnitreePoints<Object3D, double, double, double> omnitree = new OmnitreePointsLinked<Object3D, double, double, double>(locate);

			#endregion

			#region random generation

			Console.WriteLine("Generating random data...");

			Random random = new Random(0);
			int count = 100;
			Object3D[] records = new Object3D[count];
			for (int i = 0; i < count; i++)
				records[i] = new Object3D(i, random.NextDouble(), random.NextDouble(), random.NextDouble());

			Console.WriteLine("Generated random data.");

			#endregion

			#region adding

			Console.WriteLine("Building Omnitree...");

			for (int i = 0; i < count; i++)
			{
				omnitree.Add(records[i]);
				if (i % (count / 10) == 0)
					Console.WriteLine(((double)i / (double)count * 100D) + "%");
			}

			Console.WriteLine("OmniTree.Count: " + omnitree.Count);
			//Console.WriteLine("OmniTree._top.Count: " + (omnitree as OmnitreeLinked<TestObject, double>)._top.Count);

			int test_count = 0;
			omnitree.Stepper((Object3D record) => { test_count++; });
			Console.WriteLine("OmniTree Stepper Count: " + test_count);

			#endregion

			#region validation

			SetHashArray<Object3D> setHash = new SetHashArray<Object3D>(
				(Object3D a, Object3D b) => { return a.Id == b.Id; },
				(Object3D a) => { return a.Id.GetHashCode(); });
			for (int i = 0; i < count; i++)
				setHash.Add(records[i]);

			bool validated = true;
			omnitree.Stepper((Object3D record) => { if (!setHash.Contains(record)) validated = false; });
			if (validated)
				Console.WriteLine("Values Validated.");
			else
				Console.WriteLine("Values INVALID.");

			#endregion

			#region querying

			Console.WriteLine("Value Querying: ");

			bool query_test = false;
			for (int i = 0; i < count; i++)
			{
				query_test = false;
                double a, b, c;
                locate(records[i], out a, out b, out c);
				omnitree[a, b, c]((Object3D record) => { query_test = true; });
				if (query_test == false)
				{
					Console.WriteLine("Querying INVALID on value: " + i);
					break;
				}
				if (i % (count / 10) == 0)
					Console.WriteLine(((double)i / (double)count * 100D) + "%");
			}
			if (query_test == true)
				Console.WriteLine("Querying Validated.");
			else
				Console.WriteLine("Querying INVALID.");

            #endregion

            #region dynamic values (re-randomizing)

            Console.WriteLine("Moving randomized data...");

            foreach (Object3D record in records)
            {
                record.X += Math.Max(0d, Math.Min(1d, (random.NextDouble() / 100D) - .5D));
                record.Y += Math.Max(0d, Math.Min(1d, (random.NextDouble() / 100D) - .5D));
                record.Z += Math.Max(0d, Math.Min(1d, (random.NextDouble() / 100D) - .5D));
            }

            Console.WriteLine("Randomized data moved.");

            #endregion

            #region Updating

            Console.WriteLine("Updating Tree Positions...");
            //// Update Method #1
            omnitree.Update();

            //// Update Method #2
            //omnitree.Update(omnitree.Min, omnitree.Max);

            Console.WriteLine("Tree Positions Updated.");

            #endregion

            #region removal

            Console.WriteLine("Removing Values: ");
			for (int i = 0; i < count; i++)
			{
                int tempCount = omnitree.Count;

                // Removal Method #1
                omnitree.Remove(records[i]);

				//// Removal Method #2
				//omnitree.Remove(locate(records[i]), locate(records[i]));

				//// Removal Method #3
				//omnitree.Remove(locate(records[i]), locate(records[i]), (omnitree_record step) => { return records[i].Id == step.Id; });

				//// Removal Method #4
				//double[] location = new double[] { locate(records[i])(0), locate(records[i])(1), locate(records[i])(2) };
				//omnitree.Remove(location, location);

				//// Removal Method #5
				//double[] location = new double[] { locate(records[i])(0), locate(records[i])(1), locate(records[i])(2) };
				//omnitree.Remove(location, location, (omnitree_record step) => { return records[i].Id == step.Id; });

				if (omnitree.Count != count - (i + 1))
					throw new System.Exception();
				if (i % (count / 10) == 0)
					Console.WriteLine(((double)i / (double)count * 100D) + "%");
			}
			Console.WriteLine("Values Removed: ");

			Console.WriteLine("OmniTree.Count: " + omnitree.Count);

			//Console.WriteLine("OmniTree._top.Count: " + (omnitree as OmnitreeLinked<TestObject, double>)._top.Count);

			test_count = 0;
			omnitree.Stepper((Object3D record) => { test_count++; });
			Console.WriteLine("OmniTree Stepper Count: " + test_count);

			#endregion

			Console.WriteLine();
			Console.WriteLine("TEST COMPLETE");
		}

		public static void TestOmnitree2()
		{
			#region construction

			Omnitree.Location<Object3D, double, double, double, bool> Locate = (Object3D record, out double a, out double b, out double c, out bool d) =>
			{
				a = record.X;
				b = record.Y;
				c = record.Z;
				d = record.Id % 2 == 0;
			};

            Compare<double> CompareDouble = (double a, double b) =>
            {
                if (a < b)
                    return Comparison.Less;
                else if (a > b)
                    return Comparison.Greater;
                else
                    return Comparison.Equal;
            };

            Compare<bool> CompareBool = (bool a, bool b) =>
            {
                if (a == b)
                    return Comparison.Equal;
                else if (a == false)
                    return Comparison.Less;
                else
                    return Comparison.Greater;
            };

            // NEEDS OPTIMIZATION (an overload for the constructor)
            OmnitreePoints<Object3D, double, double, double, bool> omnitree = new OmnitreePointsLinked<Object3D, double, double, double, bool>(Locate);

			#endregion

			#region random generation

			Console.WriteLine("Generating random data...");

			Random random = new Random(0);
			int count = 100;
			Object3D[] records = new Object3D[count];
			for (int i = 0; i < count; i++)
				records[i] = new Object3D(i, random.NextDouble(), random.NextDouble(), random.NextDouble());

			Console.WriteLine("Generated random data.");

			#endregion

			#region adding

			Console.WriteLine("Building Omnitree...");

			for (int i = 0; i < count; i++)
			{
				omnitree.Add(records[i]);
				if (i % (count / 10) == 0)
					Console.WriteLine(((double)i / (double)count * 100D) + "%");
			}

			Console.WriteLine("Omnitree Built.");

			Console.WriteLine("OmniTree.Count: " + omnitree.Count);
			//Console.WriteLine("OmniTree._top.Count: " + (omnitree as OmnitreeLinked<TestObject, object>)._top.Count);

			int test_count = 0;
			omnitree.Stepper((Object3D record) => { test_count++; });
			Console.WriteLine("OmniTree Stepper Count: " + test_count);

			#endregion

			#region validation

			SetHashArray<Object3D> setHash = new SetHashArray<Object3D>(
				(Object3D a, Object3D b) => { return a.Id == b.Id; },
				(Object3D a) => { return a.Id.GetHashCode(); });
			for (int i = 0; i < count; i++)
				setHash.Add(records[i]);

			bool validated2 = true;
			omnitree.Stepper((Object3D record) => { if (!setHash.Contains(record)) validated2 = false; });
			if (validated2)
				Console.WriteLine("Values Validated.");
			else
				Console.WriteLine("Values INVALID.");

			#endregion

			#region querying

			Console.WriteLine("Value Querying: ");
			
			bool query_test2 = false;
			for (int i = 0; i < count; i++)
			{
				query_test2 = false;
                double a, b, c;
                bool d;
                Locate(records[i], out a, out b, out c, out d);
				omnitree[a, b, c, d]((Object3D record) => { query_test2 = true; });
				if (query_test2 == false)
				{
					Console.WriteLine("Querying INVALID on value: " + i);
					break;
				}
				if (i % (count / 10) == 0)
					Console.WriteLine(((double)i / (double)count * 100D) + "%");
			}
			if (query_test2 == true)
				Console.WriteLine("Querying Validated.");
			else
				Console.WriteLine("Querying INVALID.");

            #endregion

            #region dynamic values (re-randomizing)

            Console.WriteLine("Moving randomized data...");

            foreach (Object3D record in records)
            {
                record.X += Math.Max(0d, Math.Min(1d, (random.NextDouble() / 100D) - .5D));
                record.Y += Math.Max(0d, Math.Min(1d, (random.NextDouble() / 100D) - .5D));
                record.Z += Math.Max(0d, Math.Min(1d, (random.NextDouble() / 100D) - .5D));
            }

            Console.WriteLine("Randomized data moved.");

            #endregion

            #region updating

            Console.WriteLine("Updating Tree Positions...");
            //// Update Method #1
            omnitree.Update();

            //// Update Method #2
            //omnitree.Update(omnitree.Min, omnitree.Max);

            Console.WriteLine("Tree Positions Updated.");

            #endregion

            #region removal

            Console.WriteLine("Removing Values: ");
			for (int i = 0; i < count; i++)
			{
				//// Removal Method #1
				omnitree.Remove(records[i]);

				//// Removal Method #2
				//omnitree.Remove(locate(records[i]), locate(records[i]));

				//// Removal Method #3
				//omnitree.Remove(locate(records[i]), locate(records[i]), (omnitree_record step) => { return records[i].Id == step.Id; });

				//// Removal Method #4
				//double[] location = new double[] { locate(records[i])(0), locate(records[i])(1), locate(records[i])(2) };
				//omnitree.Remove(location, location);

				//// Removal Method #5
				//double[] location = new double[] { locate(records[i])(0), locate(records[i])(1), locate(records[i])(2) };
				//omnitree.Remove(location, location, (omnitree_record step) => { return records[i].Id == step.Id; });

				if (omnitree.Count != count - (i + 1))
					throw new System.Exception();
				if (i % (count / 10) == 0)
					Console.WriteLine(((double)i / (double)count * 100D) + "%");
			}
			Console.WriteLine("Values Removed: ");

			Console.WriteLine("OmniTree.Count: " + omnitree.Count);

			//Console.WriteLine("OmniTree._top.Count: " + (omnitree as OmnitreeLinked<TestObject, object>)._top.Count);

			test_count = 0;
			omnitree.Stepper((Object3D record) => { test_count++; });
			Console.WriteLine("OmniTree Stepper Count: " + test_count);

			#endregion

			Console.WriteLine();
			Console.WriteLine("TEST COMPLETE");
		}
	}
}
