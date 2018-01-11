using Theta;
using Theta.Mathematics;
using Theta.Structures;
using Theta.Algorithms;
using Theta.Diagnostics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Reflection;

using System.IO;

namespace Speed
{
	#region TestObject
	public class TestObject
	{
		public int Id { get; set; }
		public double X { get; set; }
		public double Y { get; set; }
		public double Z { get; set; }

		public TestObject(int id, double x, double y, double z)
		{
			this.Id = id;
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is TestObject))
				return false;
			TestObject b = obj as TestObject;
			return this.Id == b.Id && this.X == b.X && this.Y == b.Y && this.Z == b.Z;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
	#endregion

	class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Be sure to run in release mode outside of visual studio for fair speed testing!!!!!!");
            Console.WriteLine();
            Console.WriteLine();

            TestOmnitreeGiant(false);
            Console.WriteLine();
            Console.WriteLine("------------------------------");
            Console.WriteLine();
            TestOmnitreeGiant(true);
            Console.WriteLine();
            Console.WriteLine("------------------------------");
            Console.WriteLine();
            TestOmnitreeGiant(false);
            Console.WriteLine();
            Console.WriteLine("------------------------------");
            Console.WriteLine();
            TestOmnitreeGiant(true);
            Console.WriteLine("------------------------------");
            TestOmnitreeGiant_nonBulk();
            Console.WriteLine();
            Console.WriteLine("------------------------------");
            Console.WriteLine();
            TestOmnitreeGiant_nonBulk();

            #region Sorting

            //for (int testIterations = 0; testIterations < 2; testIterations++)
            //{
            //    Random random = new Random();
            //    int count = 10000000;
            //    float[] numbers = new float[count];
            //    for (int i = 0; i < count; i++)
            //        numbers[i] = (float)random.NextDouble();

            //    float[] data_A = numbers.Clone() as float[];
            //    float[] data_B = numbers.Clone() as float[];
            //    float[] data_C = numbers.Clone() as float[];

            //    DateTime start_A = DateTime.Now;
            //    Array.Sort(data_A);
            //    //float median_A = (data_A[count / 2] + data_A[(count - 1) / 2]) / 2f;
            //    float median_A = data_A[count / 2];
            //    DateTime end_A = DateTime.Now;
            //    Console.WriteLine("Median A: " + median_A);
            //    Console.WriteLine("Time A: " + (end_A - start_A));
            //    Console.WriteLine();

            //    DateTime start_B = DateTime.Now;
            //    float median_B = median(data_B);
            //    DateTime end_B = DateTime.Now;
            //    Console.WriteLine("Median B: " + median_B);
            //    Console.WriteLine("Time B: " + (end_B - start_B));
            //    Console.WriteLine();

            //    DateTime compile_start_C = DateTime.Now;
            //    float temp = Compute<float>.Median((Step<float> step) => { step(1); step(2); });
            //    DateTime compile_end_C = DateTime.Now;
            //    Console.WriteLine("Compiled... " + temp);
            //    Console.WriteLine("Compile Time: " + (compile_end_C - compile_start_C));
            //    Console.WriteLine();

            //    DateTime start_C = DateTime.Now;
            //    float median_C = Compute<float>.Median(data_C);
            //    DateTime end_C = DateTime.Now;
            //    Console.WriteLine("Median C: " + median_C);
            //    Console.WriteLine("Time C: " + (end_C - start_C));
            //    Console.WriteLine();
            //}

            #endregion

            Console.WriteLine();
			Console.WriteLine("Done...");
			Console.ReadLine();
		}

        #region testing median optimization

        private static void split(float[] a, int n, float x, ref int i, ref int j)
        {
            do
            {
                while (a[i] < x) i++;
                while (x < a[j]) j--;
                if (i <= j)
                {
                    float t = a[i];
                    a[i] = a[j];
                    a[j] = t;
                    i++; j--;
                }
            } while (i <= j);
        }

        private static void median(float[] a, int n, ref int k)
        {
            int L = 0;
            int R = n - 1;
            k = n / 2;
            int i; int j;
            while (L < R)
            {
                float x = a[k];
                i = L; j = R;
                split(a, n, x, ref i, ref j);
                if (j < k) L = i;
                if (k < i) R = j;
            }
        }

        private static float median(float[] a)
        {
            int k = 0;
            median(a, a.Length, ref k);
            return a[k];
        }

        #endregion

        #region Set

        //public static void TestSet()
        //{
        //    {
        //        int iterations = int.MaxValue / 1000;

        //        HashSet<int> validation = new HashSet<int>();
        //        //for (int i = 0; i < interations; i++)
        //        //	validation.Add(i);

        //        {
        //            HashSet<int> set0 = new HashSet<int>();
        //            SetHashList<int> set1 = new SetHashList<int>();
        //            SetHashArray<int> set2 = new SetHashArray<int>();

        //            for (int i = 0; i < iterations; i++) set0.Add(i);
        //            for (int i = 0; i < iterations; i++) set1.Add(i);
        //            for (int i = 0; i < iterations; i++) set2.Add(i);
        //            for (int i = 0; i < iterations; i++)
        //                validation.Add(i);
        //            foreach (int i in set0) { validation.Remove(i); }
        //            for (int i = 0; i < iterations; i++)
        //                validation.Add(i);
        //            set1.Stepper((int i) => { validation.Remove(i); });
        //            for (int i = 0; i < iterations; i++)
        //                validation.Add(i);
        //            set2.Stepper((int i) => { validation.Remove(i); });
        //            for (int i = 0; i < iterations; i++) set0.Contains(i);
        //            for (int i = 0; i < iterations; i++) set1.Contains(i);
        //            for (int i = 0; i < iterations; i++) set2.Contains(i);
        //            for (int i = 0; i < iterations; i++) set0.Remove(i);
        //            for (int i = 0; i < iterations; i++) set1.Remove(i);
        //            for (int i = 0; i < iterations; i++) set2.Remove(i);

        //            Console.WriteLine("Adding HashSet:               " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = 0; i < iterations; i++) set0.Add(i); }));
        //            Console.WriteLine("Adding Set_HashLinkedList:    " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = 0; i < iterations; i++) set1.Add(i); }));
        //            Console.WriteLine("Adding SetHash:               " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = 0; i < iterations; i++) set2.Add(i); }));

        //            for (int i = 0; i < iterations; i++)
        //                validation.Add(i);
        //            foreach (int i in set0) { validation.Remove(i); }
        //            Console.WriteLine("Validate HashSet:             " + (validation.Count == 0));

        //            for (int i = 0; i < iterations; i++)
        //                validation.Add(i);
        //            set1.Stepper((int i) => { validation.Remove(i); });
        //            Console.WriteLine("Validate Set_HashLinkedList:  " + (validation.Count == 0));

        //            for (int i = 0; i < iterations; i++)
        //                validation.Add(i);
        //            set2.Stepper((int i) => { validation.Remove(i); });
        //            Console.WriteLine("Validate SetHas:              " + (validation.Count == 0));

        //            Console.WriteLine("Size HashSet:                 " + (typeof(HashSet<int>).GetField("m_buckets", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(set0) as int[]).Length);
        //            Console.WriteLine("Size Set_HashLinkedList:      " + set1.TableSize);
        //            Console.WriteLine("Size SetHash:                 " + set2.TableSize);

        //            Console.WriteLine("Constains HashSet:            " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = 0; i < iterations; i++) set0.Contains(i); }));
        //            Console.WriteLine("Constains Set_HashLinkedList: " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = 0; i < iterations; i++) set1.Contains(i); }));
        //            Console.WriteLine("Constains SetHash:            " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = 0; i < iterations; i++) set2.Contains(i); }));

        //            //Console.WriteLine("Removed HashSet:              " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = 0; i < iterations; i++) set0.Remove(i); }));
        //            //Console.WriteLine("Removed Set_HashLinkedList:   " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = 0; i < iterations; i++) set1.Remove(i); }));
        //            //Console.WriteLine("Remove SetHash:               " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = 0; i < iterations; i++) set2.Remove(i); }));

        //            Console.WriteLine("Removed HashSet:              " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = iterations - 1; i >= 0; i--) set0.Remove(i); }));
        //            Console.WriteLine("Removed Set_HashLinkedList:   " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = iterations - 1; i >= 0; i--) set1.Remove(i); }));
        //            Console.WriteLine("Remove SetHash:               " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = iterations - 1; i >= 0; i--) set2.Remove(i); }));

        //            Console.WriteLine("Size HashSet:                 " + (typeof(HashSet<int>).GetField("m_buckets", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(set0) as int[]).Length);
        //            Console.WriteLine("Size Set_HashLinkedList:      " + set1.TableSize);
        //            Console.WriteLine("Size SetHash:                 " + set2.TableSize);
        //        }
        //        Console.WriteLine();
        //        {
        //            HashSet<int> set0 = new HashSet<int>();
        //            SetHashList<int> set1 = new SetHashList<int>();
        //            SetHashArray<int> set2 = new SetHashArray<int>();

        //            for (int i = 0; i < iterations; i++) set0.Add(i);
        //            for (int i = 0; i < iterations; i++) set1.Add(i);
        //            for (int i = 0; i < iterations; i++) set2.Add(i);
        //            for (int i = 0; i < iterations; i++)
        //                validation.Add(i);
        //            foreach (int i in set0) { validation.Remove(i); }
        //            for (int i = 0; i < iterations; i++)
        //                validation.Add(i);
        //            set1.Stepper((int i) => { validation.Remove(i); });
        //            for (int i = 0; i < iterations; i++)
        //                validation.Add(i);
        //            set2.Stepper((int i) => { validation.Remove(i); });
        //            for (int i = 0; i < iterations; i++) set0.Contains(i);
        //            for (int i = 0; i < iterations; i++) set1.Contains(i);
        //            for (int i = 0; i < iterations; i++) set2.Contains(i);
        //            for (int i = 0; i < iterations; i++) set0.Remove(i);
        //            for (int i = 0; i < iterations; i++) set1.Remove(i);
        //            for (int i = 0; i < iterations; i++) set2.Remove(i);

        //            Console.WriteLine("Adding HashSet:               " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = 0; i < iterations; i++) set0.Add(i); }));
        //            Console.WriteLine("Adding Set_HashLinkedList:    " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = 0; i < iterations; i++) set1.Add(i); }));
        //            Console.WriteLine("Adding SetHash:               " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = 0; i < iterations; i++) set2.Add(i); }));

        //            for (int i = 0; i < iterations; i++)
        //                validation.Add(i);
        //            foreach (int i in set0) { validation.Remove(i); }
        //            Console.WriteLine("Validate HashSet:             " + (validation.Count == 0));

        //            for (int i = 0; i < iterations; i++)
        //                validation.Add(i);
        //            set1.Stepper((int i) => { validation.Remove(i); });
        //            Console.WriteLine("Validate Set_HashLinkedList:  " + (validation.Count == 0));

        //            for (int i = 0; i < iterations; i++)
        //                validation.Add(i);
        //            set2.Stepper((int i) => { validation.Remove(i); });
        //            Console.WriteLine("Validate SetHas:              " + (validation.Count == 0));

        //            Console.WriteLine("Size HashSet:                 " + (typeof(HashSet<int>).GetField("m_buckets", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(set0) as int[]).Length);
        //            Console.WriteLine("Size Set_HashLinkedList:      " + set1.TableSize);
        //            Console.WriteLine("Size SetHash:                 " + set2.TableSize);

        //            Console.WriteLine("Constains HashSet:            " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = 0; i < iterations; i++) set0.Contains(i); }));
        //            Console.WriteLine("Constains Set_HashLinkedList: " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = 0; i < iterations; i++) set1.Contains(i); }));
        //            Console.WriteLine("Constains SetHash:            " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = 0; i < iterations; i++) set2.Contains(i); }));

        //            //Console.WriteLine("Removed HashSet:              " + Theta.Diagnostics.Performance.Time2(() => { for (int i = 0; i < iterations; i++) set0.Remove(i); }));
        //            //Console.WriteLine("Removed Set_HashLinkedList:   " + Theta.Diagnostics.Performance.Time2(() => { for (int i = 0; i < iterations; i++) set1.Remove(i); }));
        //            //Console.WriteLine("Remove SetHash:               " + Theta.Diagnostics.Performance.Time2(() => { for (int i = 0; i < iterations; i++) set2.Remove(i); }));

        //            Console.WriteLine("Removed HashSet:              " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = iterations - 1; i >= 0; i--) set0.Remove(i); }));
        //            Console.WriteLine("Removed Set_HashLinkedList:   " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = iterations - 1; i >= 0; i--) set1.Remove(i); }));
        //            Console.WriteLine("Remove SetHash:               " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = iterations - 1; i >= 0; i--) set2.Remove(i); }));

        //            Console.WriteLine("Size HashSet:                 " + (typeof(HashSet<int>).GetField("m_buckets", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(set0) as int[]).Length);
        //            Console.WriteLine("Size Set_HashLinkedList:      " + set1.TableSize);
        //            Console.WriteLine("Size SetHash:                 " + set2.TableSize);
        //        }

        //        Console.WriteLine();
        //    }
        //}

        #endregion

        #region Map

        //public static void TestMap()
        //{
        //    int iterations = int.MaxValue / 10000;

        //    HashSet<int> validation = new HashSet<int>();
        //    //for (int i = 0; i < interations; i++)
        //    //	validation.Add(i);

        //    {
        //        Dictionary<int, int> map0 = new Dictionary<int, int>();
        //        //MapSetHashList<int, int> map1 = new MapSetHashList<int, int>();
        //        MapHashLinked<int, int> map2 = new MapHashLinked<int, int>();
        //        MapHashArray<int, int> map3 = new MapHashArray<int, int>();


        //        Console.WriteLine("Adding 0:    " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = 0; i < iterations; i++) map0.Add(i, i); }));
        //        //Console.WriteLine("Adding 1:    " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = 0; i < iterations; i++) map1.Add(i, i); }));
        //        Console.WriteLine("Adding 2:    " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = 0; i < iterations; i++) map2.Add(i, i); }));
        //        Console.WriteLine("Adding 3:    " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = 0; i < iterations; i++) map3.Add(i, i); }));

        //        for (int i = 0; i < iterations; i++)
        //            validation.Add(i);
        //        foreach (KeyValuePair<int, int> i in map0) { validation.Remove(i.Key); }
        //        Console.WriteLine("Validate 0:  " + (validation.Count == 0));

        //        //for (int i = 0; i < iterations; i++)
        //        //	validation.Add(i);
        //        ////foreach (int i in map1) { validation.Remove(i); }
        //        //map1.Stepper((int i) => { validation.Remove(i); });
        //        //Console.WriteLine("Validate 1:  " + (validation.Count == 0));

        //        for (int i = 0; i < iterations; i++)
        //            validation.Add(i);
        //        //foreach (int i in map1) { validation.Remove(i); }
        //        map2.Stepper((int i) => { validation.Remove(i); });
        //        Console.WriteLine("Validate 2:  " + (validation.Count == 0));

        //        for (int i = 0; i < iterations; i++)
        //            validation.Add(i);
        //        //foreach (int i in map1) { validation.Remove(i); }
        //        map3.Stepper((int i) => { validation.Remove(i); });
        //        Console.WriteLine("Validate 3:  " + (validation.Count == 0));

        //        int temp;
        //        Console.WriteLine("Get 0:       " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = 0; i < iterations; i++) temp = map0[i]; }));
        //        //Console.WriteLine("Get 1:       " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = 0; i < iterations; i++) temp = map1[i]; }));
        //        Console.WriteLine("Get 2:       " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = 0; i < iterations; i++) temp = map2[i]; }));
        //        Console.WriteLine("Get 3:       " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = 0; i < iterations; i++) temp = map3[i]; }));

        //        Console.WriteLine("Removed 0:   " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = 0; i < iterations; i++) map0.Remove(i); }));
        //        //Console.WriteLine("Removed 1:   " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = 0; i < iterations; i++) map1.Remove(i); }));
        //        Console.WriteLine("Removed 2:   " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = 0; i < iterations; i++) map2.Remove(i); }));
        //        Console.WriteLine("Removed 3:   " + Theta.Diagnostics.Performance.Time_StopWatch(() => { for (int i = 0; i < iterations; i++) map3.Remove(i); }));
        //    }

        //}

        #endregion

        #region Sorting

        //public static void TestSorting()
        //{
        //    int size = int.MaxValue / 1000000;
        //    int[] dataSet = new int[size];
        //    for (int i = 0; i < size; i++)
        //        dataSet[i] = i;

        //    Console.WriteLine("Sorting Algorithms----------------------");
        //    Console.WriteLine();

        //    //Sort<int>.Shuffle(dataSet);
        //    //Console.Write("Bubble:      " + Theta.Diagnostics.Performance.Time_StopWatch(() => { Sort<int>.Bubble(dataSet); }));
        //    Sort<int>.Shuffle(dataSet);
        //    Console.WriteLine("Selection:   " + Theta.Diagnostics.Performance.Time_StopWatch(() => { Sort<int>.Selection(dataSet); }));
        //    Sort<int>.Shuffle(dataSet);
        //    Console.WriteLine("Insertion:   " + Theta.Diagnostics.Performance.Time_StopWatch(() => { Sort<int>.Insertion(dataSet); }));
        //    Sort<int>.Shuffle(dataSet);
        //    Console.WriteLine("Quick:       " + Theta.Diagnostics.Performance.Time_StopWatch(() => { Sort<int>.Quick(dataSet); }));
        //    Sort<int>.Shuffle(dataSet);
        //    Console.WriteLine("Merge:       " + Theta.Diagnostics.Performance.Time_StopWatch(() => { Sort<int>.Merge(dataSet); }));
        //    Sort<int>.Shuffle(dataSet);
        //    Console.WriteLine("Heap:        " + Theta.Diagnostics.Performance.Time_StopWatch(() => { Sort<int>.Heap(dataSet); }));
        //    Sort<int>.Shuffle(dataSet);
        //    Console.WriteLine("OddEven:     " + Theta.Diagnostics.Performance.Time_StopWatch(() => { Sort<int>.OddEven(dataSet); }));

        //    Sort<int>.Shuffle(dataSet);
        //    Console.WriteLine("IEnumerable: " + Theta.Diagnostics.Performance.Time_StopWatch(() => { dataSet.OrderBy(item => item); }));
        //    Sort<int>.Shuffle(dataSet);
        //    Console.WriteLine("Array.Sort:  " + Theta.Diagnostics.Performance.Time_StopWatch(() => { Array.Sort(dataSet); }));
        //}

        #endregion

        public static void TestOmnitreeGiant(bool multithread)
        {
            ulong count = 100000;

            bool query_single = true;

            Console.WriteLine("TestOmnitreeGiant [" + count + " records] [multi = " + multithread + "]...");
            Console.WriteLine();

            OmnitreePointsLinked<TestObject, double, double, double> omnitree_3D_2 = new OmnitreePointsLinked<TestObject, double, double, double>(
                    (TestObject record, out double x, out double y, out double z) =>
                    {
                        x = record.X;
                        y = record.Y;
                        z = record.Z;
                    }
                //,
                //(Omnitree.Bounds<double, double, double> bounds, Stepper<TestObject> stepper) =>
                //{
                //    double min = bounds.Min1.Exists ? bounds.Min1.Value : 0;
                //    double max = bounds.Max1.Exists ? bounds.Max1.Value : 1;
                //    return (min + max) / 2;
                //},
                //(Omnitree.Bounds<double, double, double> bounds, Stepper<TestObject> stepper) =>
                //{
                //    double min = bounds.Min2.Exists ? bounds.Min2.Value : 0;
                //    double max = bounds.Max2.Exists ? bounds.Max2.Value : 1;
                //    return (min + max) / 2;
                //},
                //(Omnitree.Bounds<double, double, double> bounds, Stepper<TestObject> stepper) =>
                //{
                //    double min = bounds.Min3.Exists ? bounds.Min3.Value : 0;
                //    double max = bounds.Max3.Exists ? bounds.Max3.Value : 1;
                //    return (min + max) / 2;
                //}
                    );

            Random random = new Random(7);
            BigArray<TestObject> records = new BigArray<TestObject>(count);
            for (ulong i = 0; i < count; i++)
                records[i] = new TestObject((int)i, random.NextDouble(), random.NextDouble(), random.NextDouble());

            Console.WriteLine("Adding:                " + Theta.Diagnostics.Performance.Time_StopWatch(() => { omnitree_3D_2.Add(records, multithread); }));
            Console.WriteLine("Max Depth:             " + omnitree_3D_2.MaxDepth);
            Console.WriteLine("Node Count:            " + omnitree_3D_2.NodeCount);
            Console.WriteLine("Branch Count:          " + omnitree_3D_2.BranchCount);
            Console.WriteLine("Leaf Count:            " + omnitree_3D_2.LeafCount);

            ulong query_max = (ulong)Math.Min(1000, (double)count * .1);
            if (query_single)
            {
                Console.WriteLine("Querying(" + query_max + "): " + Theta.Diagnostics.Performance.Time_StopWatch(() =>
                {
                    bool query_test;
                    for (ulong i = 0; i < query_max; i++)
                    {
                        query_test = false;
                        omnitree_3D_2[records[i].X, records[i].Y, records[i].Z]((TestObject record) => { query_test = true; });
                        if (query_test == false)
                            throw new System.Exception();
                    }
                }));
            }

            OmnitreePointsLinked<TestObject, double, double, double> clone;
            Console.WriteLine("Clone: " + Theta.Diagnostics.Performance.Time_StopWatch(() =>
            {
                clone = omnitree_3D_2.Clone();
                if (
                    clone.Count != omnitree_3D_2.Count ||
                    clone.LeafCount != omnitree_3D_2.LeafCount ||
                    clone.BranchCount != omnitree_3D_2.BranchCount ||
                    clone.NodeCount != omnitree_3D_2.NodeCount ||
                    clone.MaxDepth != omnitree_3D_2.MaxDepth)
                {
                    throw new Exception("Clone method invalid");
                }
            }));
        }

        public static void TestOmnitreeGiant_nonBulk()
        {
            ulong count = 100000;

            bool query_single = true;

            Console.WriteLine("TestOmnitreeGiant_nonBulk [" + count + " records]...");
            Console.WriteLine();

            OmnitreePointsLinked<TestObject, double, double, double> omnitree_3D_2 = new OmnitreePointsLinked<TestObject, double, double, double>(
                    (TestObject record, out double x, out double y, out double z) =>
                    {
                        x = record.X;
                        y = record.Y;
                        z = record.Z;
                    }
                //,
                //(Omnitree.Bounds<double, double, double> bounds, Stepper<TestObject> stepper) =>
                //{
                //    double min = bounds.Min1.Exists ? bounds.Min1.Value : 0;
                //    double max = bounds.Max1.Exists ? bounds.Max1.Value : 1;
                //    return (min + max) / 2;
                //},
                //(Omnitree.Bounds<double, double, double> bounds, Stepper<TestObject> stepper) =>
                //{
                //    double min = bounds.Min2.Exists ? bounds.Min2.Value : 0;
                //    double max = bounds.Max2.Exists ? bounds.Max2.Value : 1;
                //    return (min + max) / 2;
                //},
                //(Omnitree.Bounds<double, double, double> bounds, Stepper<TestObject> stepper) =>
                //{
                //    double min = bounds.Min3.Exists ? bounds.Min3.Value : 0;
                //    double max = bounds.Max3.Exists ? bounds.Max3.Value : 1;
                //    return (min + max) / 2;
                //}
                    );

            Random random = new Random(7);
            BigArray<TestObject> records = new BigArray<TestObject>(count);
            for (ulong i = 0; i < count; i++)
                records[i] = new TestObject((int)i, random.NextDouble(), random.NextDouble(), random.NextDouble());

            Console.WriteLine("Adding:                " + Theta.Diagnostics.Performance.Time_StopWatch(() => { foreach (TestObject obj in records) omnitree_3D_2.Add(obj); }));
            Console.WriteLine("Max Depth:             " + omnitree_3D_2.MaxDepth);
            Console.WriteLine("Node Count:            " + omnitree_3D_2.NodeCount);
            Console.WriteLine("Branch Count:          " + omnitree_3D_2.BranchCount);
            Console.WriteLine("Leaf Count:            " + omnitree_3D_2.LeafCount);

            ulong query_max = (ulong)Math.Min(1000, (double)count * .1);
            if (query_single)
            {
                Console.WriteLine("Querying(" + query_max + "): " + Theta.Diagnostics.Performance.Time_StopWatch(() =>
                {
                    bool query_test;
                    for (ulong i = 0; i < query_max; i++)
                    {
                        query_test = false;
                        omnitree_3D_2[records[i].X, records[i].Y, records[i].Z]((TestObject record) => { query_test = true; });
                        if (query_test == false)
                            throw new System.Exception();
                    }
                }));
            }

            OmnitreePointsLinked<TestObject, double, double, double> clone;
            Console.WriteLine("Clone: " + Theta.Diagnostics.Performance.Time_StopWatch(() =>
            {
                clone = omnitree_3D_2.Clone();
                if (
                    clone.Count != omnitree_3D_2.Count ||
                    clone.LeafCount != omnitree_3D_2.LeafCount ||
                    clone.BranchCount != omnitree_3D_2.BranchCount ||
                    clone.NodeCount != omnitree_3D_2.NodeCount ||
                    clone.MaxDepth != omnitree_3D_2.MaxDepth)
                {
                    throw new Exception("Clone method invalid");
                }
            }));
        }

        #region TestMultiDimensional Comparisons

        //public static void TestMultiDimensional()
        //{
        //    int count = 1000000;

        //    Random random = new Random();
        //    TestObject[] records = new TestObject[count];
        //    int[] random_order = new int[records.Length];
        //    for (int i = 0; i < count; i++)
        //    {
        //        records[i] = new TestObject(i, random.NextDouble(), random.NextDouble(), random.NextDouble());
        //        random_order[i] = i;
        //    }
        //    Theta.Algorithms.Sort<int>.Shuffle(random, random_order);
        //    int random_query_count = 1;
        //    double[][] random_mins = new double[random_query_count][];
        //    for (int i = 0; i < random_query_count; i++)
        //        random_mins[i] = new double[] { random.NextDouble(), random.NextDouble(), random.NextDouble(), };
        //    double[][] random_maxes = new double[random_query_count][];
        //    for (int i = 0; i < random_query_count; i++)
        //        random_maxes[i] = new double[] { 
        //            (random.NextDouble() * (1 - random_mins[i][0])) + random_mins[i][0], 
        //            (random.NextDouble() * (1 - random_mins[i][1])) + random_mins[i][1],
        //            (random.NextDouble() * (1 - random_mins[i][2])) + random_mins[i][2] };

        //    int[] expected_query_counts;
        //    Console.WriteLine("Testing Omnitree...");
        //    TestMultiDimensional_Omnitree(records, random_order, random_mins, random_maxes, out expected_query_counts, false);
        //    Console.WriteLine();
        //    //Console.WriteLine("Testing List...");
        //    //TestMultiDimensional_List(records, random_order, random_mins, random_maxes, expected_query_counts);
        //    //Console.WriteLine();
        //    //Console.WriteLine("Testing LinkedList...");
        //    //TestMultiDimensional_LinkedList(records, random_order, random_mins, random_maxes, expected_query_counts);
        //    //Console.WriteLine();
        //    Console.WriteLine("Testing Dictionary...");
        //    TestMultiDimensional_Dictionary(records, random_order, random_mins, random_maxes, expected_query_counts);
        //}

        //public static void TestMultiDimensional_Omnitree(
        //    TestObject[] records,
        //    int[] random_order,
        //    double[][] random_mins,
        //    double[][] random_maxes,
        //    out int[] expected_query_range_counts,
        //    bool allowMultithreading)
        //{
        //    OmnitreePoints_2<TestObject, double, double, double> omnitree_3D_2 = new OmnitreePoints_2<TestObject, double, double, double>(
        //            (TestObject record, out double x, out double y, out double z) =>
        //            {
        //                x = record.X;
        //                y = record.Y;
        //                z = record.Z;
        //            }
        //            ,
        //            (Omnitree.Bounds<double, double, double> bounds, Stepper<TestObject> stepper) =>
        //            {
        //                double min = bounds.Min1.Exists ? bounds.Min1.Value : 0;
        //                double max = bounds.Max1.Exists ? bounds.Max1.Value : 1;
        //                return (min + max) / 2;
        //            },
        //            (Omnitree.Bounds<double, double, double> bounds, Stepper<TestObject> stepper) =>
        //            {
        //                double min = bounds.Min2.Exists ? bounds.Min2.Value : 0;
        //                double max = bounds.Max2.Exists ? bounds.Max2.Value : 1;
        //                return (min + max) / 2;
        //            },
        //            (Omnitree.Bounds<double, double, double> bounds, Stepper<TestObject> stepper) =>
        //            {
        //                double min = bounds.Min3.Exists ? bounds.Min3.Value : 0;
        //                double max = bounds.Max3.Exists ? bounds.Max3.Value : 1;
        //                return (min + max) / 2;
        //            }
        //            );

        //    Console.WriteLine("Adding:           " + Theta.Diagnostics.Performance.Time_StopWatch(() =>
        //    {
        //        //foreach (TestObject obj in records)
        //        //	omnitree_3D_2.Add(obj);
        //        omnitree_3D_2.Add(allowMultithreading, records);
        //    }));

        //    Console.WriteLine("Querying Singles: " + Theta.Diagnostics.Performance.Time_StopWatch(() =>
        //    {
        //        bool query_test;
        //        for (int i = 0; i < records.Length; i++)
        //        {
        //            query_test = false;
        //            omnitree_3D_2[records[random_order[i]].X, records[random_order[i]].Y, records[random_order[i]].Z]((TestObject record) => { query_test = true; });
        //            if (query_test == false)
        //                throw new System.Exception();
        //        }
        //    }));

        //    int[] randomquery_range_counts = new int[random_mins.Length];
        //    Console.WriteLine("Querying Ranges:  " + Theta.Diagnostics.Performance.Time_StopWatch(() =>
        //    {
        //        for (int i = 0; i < random_mins.Length; i++)
        //        {
        //            omnitree_3D_2.Stepper((TestObject record) => { randomquery_range_counts[i]++; }, random_mins[i][0], random_maxes[i][0], random_mins[i][1], random_maxes[i][1], random_mins[i][2], random_maxes[i][2]);
        //        }
        //    }));
        //    expected_query_range_counts = randomquery_range_counts;

        //    Console.WriteLine("Removing Singles: " + Theta.Diagnostics.Performance.Time_StopWatch(() =>
        //    {
        //        for (int i = 0; i < records.Length; i++)
        //            omnitree_3D_2.Remove(records[random_order[i]]);
        //    }));

        //    TimeSpan omnitree_3D_2_remove_span = TimeSpan.Zero;
        //    for (int i = 0; i < random_mins.Length; i++)
        //    {
        //        omnitree_3D_2 = new OmnitreePoints_2<TestObject, double, double, double>(
        //            (TestObject record, out double x, out double y, out double z) =>
        //            {
        //                x = record.X;
        //                y = record.Y;
        //                z = record.Z;
        //            });
        //        omnitree_3D_2.Add(allowMultithreading, records);

        //        omnitree_3D_2_remove_span += Theta.Diagnostics.Performance.Time_StopWatch(() =>
        //        {
        //            omnitree_3D_2.Remove(random_mins[i][0], random_maxes[i][0], random_mins[i][1], random_maxes[i][1], random_mins[i][2], random_maxes[i][2]);
        //        });
        //    }
        //    Console.WriteLine("Removing Ranges:  " + omnitree_3D_2_remove_span);

        //    Console.WriteLine("Update:           " + Theta.Diagnostics.Performance.Time_StopWatch(() =>
        //    {
        //        omnitree_3D_2.Update();
        //    }));
        //}

        //public static void TestMultiDimensional_List(
        //    TestObject[] records,
        //    int[] random_order,
        //    double[][] random_mins,
        //    double[][] random_maxes,
        //    int[] expected_query_range_counts)
        //{
        //    System.Collections.Generic.List<TestObject> list = new System.Collections.Generic.List<TestObject>(records.Length);

        //    Console.WriteLine("Adding:           " + Theta.Diagnostics.Performance.Time_StopWatch(() =>
        //    {
        //        for (int i = 0; i < records.Length; i++)
        //            list.Add(records[i]);
        //    }));

        //    Console.WriteLine("Querying Singles: " + Theta.Diagnostics.Performance.Time_StopWatch(() =>
        //    {
        //        bool query_test = false;
        //        for (int i = 0; i < records.Length; i++)
        //        {
        //            foreach (TestObject record in list)
        //            {
        //                if (record.X == records[random_order[i]].X && record.Y == records[random_order[i]].Y && record.Z == records[random_order[i]].Z)
        //                {
        //                    query_test = true;
        //                    break;
        //                }
        //            }
        //            if (query_test == false)
        //                throw new System.Exception();
        //        }
        //    }));

        //    int[] randomquery_range_counts = new int[random_mins.Length];
        //    Console.WriteLine("Querying Ranges:  " + Theta.Diagnostics.Performance.Time_StopWatch(() =>
        //    {
        //        for (int i = 0; i < random_mins.Length; i++)
        //        {
        //            foreach (TestObject record in list)
        //            {
        //                if (record.X >= random_mins[i][0] && record.X <= random_maxes[i][0] &&
        //                    record.Y >= random_mins[i][1] && record.Y <= random_maxes[i][1] &&
        //                    record.Z >= random_mins[i][2] && record.Z <= random_maxes[i][2])
        //                {
        //                    randomquery_range_counts[i]++;
        //                }
        //            }
        //        }
        //    }));

        //    for (int i = 0; i < random_mins.Length; i++)
        //        if (expected_query_range_counts[i] != randomquery_range_counts[i])
        //            Console.WriteLine("RANGED COUNTS MISMATCH ERROR");

        //    Console.WriteLine("Removing Singles: " + Theta.Diagnostics.Performance.Time_StopWatch(() =>
        //    {
        //        for (int i = 0; i < records.Length; i++)
        //        {
        //            for (int j = 0; j < list.Count; j++)
        //            {
        //                if (list[j].X == records[i].X && list[j].Y == records[i].Y && list[j].Z == records[i].Z)
        //                {
        //                    list.RemoveAt(j);
        //                    break;
        //                }
        //            }
        //        }
        //    }));

        //    TimeSpan list_remove_span = TimeSpan.Zero;
        //    for (int i = 0; i < random_mins.Length; i++)
        //    {
        //        list = new System.Collections.Generic.List<TestObject>(records.Length);
        //        for (int j = 0; j < records.Length; j++)
        //            list.Add(records[j]);

        //        list_remove_span += Theta.Diagnostics.Performance.Time_StopWatch(() =>
        //        {
        //            System.Collections.Generic.List<TestObject> temp = new System.Collections.Generic.List<TestObject>();

        //            foreach (TestObject record in list)
        //            {
        //                if (record.X >= random_mins[i][0] && record.X <= random_maxes[i][0] &&
        //                    record.Y >= random_mins[i][1] && record.Y <= random_maxes[i][1] &&
        //                    record.Z >= random_mins[i][2] && record.Z <= random_maxes[i][2])
        //                {
        //                    temp.Add(record);
        //                }
        //            }
        //            list = temp;
        //        });
        //    }
        //    Console.WriteLine("Removing Ranges:  " + list_remove_span);
        //}

        //public static void TestMultiDimensional_LinkedList(
        //    TestObject[] records,
        //    int[] random_order,
        //    double[][] random_mins,
        //    double[][] random_maxes,
        //    int[] expected_query_range_counts)
        //{
        //    System.Collections.Generic.LinkedList<TestObject> list = new System.Collections.Generic.LinkedList<TestObject>();

        //    Console.WriteLine("Adding:           " + Theta.Diagnostics.Performance.Time_StopWatch(() =>
        //    {
        //        for (int i = 0; i < records.Length; i++)
        //            list.AddLast(records[i]);
        //    }));

        //    Console.WriteLine("Querying Singles: " + Theta.Diagnostics.Performance.Time_StopWatch(() =>
        //    {
        //        bool query_test = false;
        //        for (int i = 0; i < records.Length; i++)
        //        {
        //            foreach (TestObject record in list)
        //            {
        //                if (record.X == records[random_order[i]].X && record.Y == records[random_order[i]].Y && record.Z == records[random_order[i]].Z)
        //                {
        //                    query_test = true;
        //                    break;
        //                }
        //            }
        //            if (query_test == false)
        //                throw new System.Exception();
        //        }
        //    }));

        //    int[] randomquery_range_counts = new int[random_mins.Length];
        //    Console.WriteLine("Querying Ranges:  " + Theta.Diagnostics.Performance.Time_StopWatch(() =>
        //    {
        //        for (int i = 0; i < random_mins.Length; i++)
        //        {
        //            foreach (TestObject record in list)
        //            {
        //                if (record.X >= random_mins[i][0] && record.X <= random_maxes[i][0] &&
        //                    record.Y >= random_mins[i][1] && record.Y <= random_maxes[i][1] &&
        //                    record.Z >= random_mins[i][2] && record.Z <= random_maxes[i][2])
        //                {
        //                    randomquery_range_counts[i]++;
        //                }
        //            }
        //        }
        //    }));

        //    for (int i = 0; i < random_mins.Length; i++)
        //        if (expected_query_range_counts[i] != randomquery_range_counts[i])
        //            Console.WriteLine("RANGED COUNTS MISMATCH ERROR");

        //    Console.WriteLine("Removing Singles: " + Theta.Diagnostics.Performance.Time_StopWatch(() =>
        //        {
        //            for (int i = 0; i < records.Length; i++)
        //            {
        //                list.Remove(records[random_order[i]]);
        //            }
        //        }));

        //    TimeSpan list_remove_span = TimeSpan.Zero;
        //    for (int i = 0; i < random_mins.Length; i++)
        //    {
        //        list = new System.Collections.Generic.LinkedList<TestObject>();
        //        for (int j = 0; j < records.Length; j++)
        //            list.AddLast(records[j]);

        //        list_remove_span += Theta.Diagnostics.Performance.Time_StopWatch(() =>
        //        {
        //            System.Collections.Generic.LinkedList<TestObject> temp2 = new System.Collections.Generic.LinkedList<TestObject>();

        //            foreach (TestObject record in list)
        //            {
        //                if (!(record.X >= random_mins[i][0] && record.X <= random_maxes[i][0] &&
        //                    record.Y >= random_mins[i][1] && record.Y <= random_maxes[i][1] &&
        //                    record.Z >= random_mins[i][2] && record.Z <= random_maxes[i][2]))
        //                {
        //                    temp2.AddLast(record);
        //                }
        //            }
        //        });
        //    }
        //    Console.WriteLine("Removing Ranges:  " + list_remove_span);
        //}

        //public static void TestMultiDimensional_Dictionary(
        //    TestObject[] records,
        //    int[] random_order,
        //    double[][] random_mins,
        //    double[][] random_maxes,
        //    int[] expected_query_range_counts)
        //{
        //    System.Collections.Generic.Dictionary<Vector<double>, TestObject> dictionary = new System.Collections.Generic.Dictionary<Vector<double>, TestObject>(records.Length);

        //    Console.WriteLine("Adding:           " + Theta.Diagnostics.Performance.Time_StopWatch(() =>
        //    {
        //        for (int i = 0; i < records.Length; i++)
        //            dictionary.Add(new Vector<double>(records[i].X, records[i].Y, records[i].Z), records[i]);
        //    }));

        //    Console.WriteLine("Querying Singles: " + Theta.Diagnostics.Performance.Time_StopWatch(() =>
        //    {
        //        for (int i = 0; i < records.Length; i++)
        //        {
        //            TestObject test = dictionary[new Vector<double>(records[random_order[i]].X, records[random_order[i]].Y, records[random_order[i]].Z)];
        //            if (test == null)
        //                Console.WriteLine("INVALID SINGLE QUERY DICTIONARY ERROR");
        //        }
        //    }));

        //    int[] randomquery_range_counts = new int[random_mins.Length];
        //    Console.WriteLine("Querying Ranges:  " + Theta.Diagnostics.Performance.Time_StopWatch(() =>
        //    {
        //        for (int i = 0; i < random_mins.Length; i++)
        //        {
        //            foreach (KeyValuePair<Vector<double>, TestObject> record in dictionary)
        //            {
        //                if (record.Value.X >= random_mins[i][0] && record.Value.X <= random_maxes[i][0] &&
        //                    record.Value.Y >= random_mins[i][1] && record.Value.Y <= random_maxes[i][1] &&
        //                    record.Value.Z >= random_mins[i][2] && record.Value.Z <= random_maxes[i][2])
        //                {
        //                    randomquery_range_counts[i]++;
        //                }
        //            }
        //        }
        //    }));

        //    for (int i = 0; i < random_mins.Length; i++)
        //        if (expected_query_range_counts[i] != randomquery_range_counts[i])
        //            Console.WriteLine("RANGED COUNTS MISMATCH ERROR");

        //    Console.WriteLine("Removing Singles: " + Theta.Diagnostics.Performance.Time_StopWatch(() =>
        //    {
        //        for (int i = 0; i < records.Length; i++)
        //        {
        //            dictionary.Remove(new Vector<double>(records[random_order[i]].X, records[random_order[i]].Y, records[random_order[i]].Z));
        //        }
        //    }));

        //    TimeSpan dictionary_remove_span = TimeSpan.Zero;
        //    for (int i = 0; i < random_mins.Length; i++)
        //    {
        //        dictionary = new System.Collections.Generic.Dictionary<Vector<double>, TestObject>(records.Length);
        //        for (int j = 0; j < records.Length; j++)
        //            dictionary.Add(new Vector<double>(records[j].X, records[j].Y, records[j].Z), records[j]);

        //        dictionary_remove_span += Theta.Diagnostics.Performance.Time_StopWatch(() =>
        //        {
        //            System.Collections.Generic.Dictionary<Vector<double>, TestObject> temp =
        //                new System.Collections.Generic.Dictionary<Vector<double>, TestObject>(records.Length);

        //            foreach (KeyValuePair<Vector<double>, TestObject> record in dictionary)
        //            {
        //                if (!(record.Value.X >= random_mins[i][0] && record.Value.X <= random_maxes[i][0] &&
        //                    record.Value.Y >= random_mins[i][1] && record.Value.Y <= random_maxes[i][1] &&
        //                    record.Value.Z >= random_mins[i][2] && record.Value.Z <= random_maxes[i][2]))
        //                {
        //                    temp.Add(record.Key, record.Value);
        //                }
        //            }
        //            dictionary = temp;
        //        });
        //    }
        //    Console.WriteLine("Removing Ranges:  " + dictionary_remove_span);
        //}

        #endregion
    }
}
