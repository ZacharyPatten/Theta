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
	#region IntegerClass

	public class IntegerClass
	{
		public int _int;

		public IntegerClass(int integer)
		{
			this._int = integer;
		}

		public static bool operator <(IntegerClass a, IntegerClass b)
		{
			return a._int < b._int;
		}

		public static bool operator >(IntegerClass a, IntegerClass b)
		{
			return a._int > b._int;
		}

		public static bool operator ==(IntegerClass a, IntegerClass b)
		{
			if (object.ReferenceEquals(a, null))
				if (object.ReferenceEquals(b, null))
					return true;
				else
					return false;
			if (object.ReferenceEquals(b, null))
				return false;
			return a._int == b._int;
		}

		public static bool operator !=(IntegerClass a, IntegerClass b)
		{
			if (object.ReferenceEquals(a, null))
				if (object.ReferenceEquals(b, null))
					return false;
				else
					return true;
			if (object.ReferenceEquals(b, null))
				return true;
			return a._int != b._int;
		}

		public override bool Equals(object obj)
		{
			if (obj is IntegerClass)
				return this == (obj as IntegerClass);
			return base.Equals(obj);
		}

		public static implicit operator IntegerClass(int integer)
		{
			return new IntegerClass(integer);
		}

		public override int GetHashCode()
		{
			return this._int % 10;
			//return base.GetHashCode();
		}
	}

	#endregion

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
        #region hide

        static string error_location = string.Empty;

		static void Test()
		{
			Console.WriteLine();
		}

        public enum blah
        {
            a,
            b,
            c,
            d,
            e,
        }

        #endregion

        static void Main(string[] args)
        {
            #region hide

            //foreach (Type type in typeof(blah).GetInterfaces())
            //{
            //    Console.WriteLine(type);
            //}
            //Console.ReadLine();

			//int count = 1000;
			//Random random = new Random(7);
			//float[] values1 = new float[count];
			//for (int i = 0; i < count; i++)
			//	values1[i] = (float)random.NextDouble();
			//float[] values2 = values1.Clone() as float[];
			//float[] values3 = values1.Clone() as float[];

			//DateTime start1 = DateTime.Now;
			//int k = values1.Length / 2;
			//median(values1, values1.Length, ref k);
			//DateTime end1 = DateTime.Now;
			//Console.WriteLine("Time: " + (end1 - start1));
			//Console.WriteLine("Value: " + values1[k]);

			//DateTime start2 = DateTime.Now;
			//Array.Sort(values2);
			//DateTime end2 = DateTime.Now;
			//Console.WriteLine("Time: " + (end2 - start2));
			//Console.WriteLine("Value: " + values2[(values2.Length - 1) / 2]);




            //TestMultiDimensional();
            //Console.WriteLine();
            //Console.WriteLine("------------------------------");
            //Console.WriteLine();
            //TestMultiDimensional();

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



            //TestOmnitree58();

            Console.WriteLine();
			Console.WriteLine("Done...");
			Console.ReadLine();
		}

        public class Record
        {
            public double word_freq_make;
            public double word_freq_address;
            public double word_freq_all;
            public double word_freq_3d;
            public double word_freq_our;
            public double word_freq_over;
            public double word_freq_remove;
            public double word_freq_internet;
            public double word_freq_order;
            public double word_freq_mail;
            public double word_freq_receive;
            public double word_freq_will;
            public double word_freq_people;
            public double word_freq_report;
            public double word_freq_addresses;
            public double word_freq_free;
            public double word_freq_business;
            public double word_freq_email;
            public double word_freq_you;
            public double word_freq_credit;
            public double word_freq_your;
            public double word_freq_font;
            public double word_freq_000;
            public double word_freq_money;
            public double word_freq_hp;
            public double word_freq_hpl;
            public double word_freq_george;
            public double word_freq_650;
            public double word_freq_lab;
            public double word_freq_labs;
            public double word_freq_telnet;
            public double word_freq_857;
            public double word_freq_data;
            public double word_freq_415;
            public double word_freq_85;
            public double word_freq_technology;
            public double word_freq_1999;
            public double word_freq_parts;
            public double word_freq_pm;
            public double word_freq_direct;
            public double word_freq_cs;
            public double word_freq_meeting;
            public double word_freq_original;
            public double word_freq_project;
            public double word_freq_re;
            public double word_freq_edu;
            public double word_freq_table;
            public double word_freq_conference;
            public double char_freq_semicolon;
            public double char_freq_openparenthases;
            public double char_freq_openbracket;
            public double char_freq_exclamation;
            public double char_freq_dollarsign;
            public double char_freq_pound;
            public double capital_run_length_average;
            public double capital_run_length_longest;
            public double capital_run_length_total;
            public bool is_spam;
						
						public Record(
                            double word_freq_make,
							double word_freq_address,
							double word_freq_all,
							double word_freq_3d,
							double word_freq_our,
							double word_freq_over,
							double word_freq_remove,
							double word_freq_internet,
							double word_freq_order,
							double word_freq_mail,
							double word_freq_receive,
							double word_freq_will,
							double word_freq_people,
							double word_freq_report,
							double word_freq_addresses,
							double word_freq_free,
							double word_freq_business,
							double word_freq_email,
							double word_freq_you,
							double word_freq_credit,
							double word_freq_your,
							double word_freq_font,
							double word_freq_000,
							double word_freq_money,
							double word_freq_hp,
							double word_freq_hpl,
							double word_freq_george,
							double word_freq_650,
							double word_freq_lab,
							double word_freq_labs,
							double word_freq_telnet,
							double word_freq_857,
							double word_freq_data,
							double word_freq_415,
							double word_freq_85,
							double word_freq_technology,
							double word_freq_1999,
							double word_freq_parts,
							double word_freq_pm,
							double word_freq_direct,
							double word_freq_cs,
							double word_freq_meeting,
							double word_freq_original,
							double word_freq_project,
							double word_freq_re,
							double word_freq_edu,
							double word_freq_table,
							double word_freq_conference,
							double char_freq_semicolon,
							double char_freq_openparenthases,
							double char_freq_openbracket,
							double char_freq_exclamation,
							double char_freq_dollarsign,
							double char_freq_pound,
							double capital_run_length_average,
							double capital_run_length_longest,
							double capital_run_length_total,
							bool is_spam)
						{
							this.word_freq_make = word_freq_make;
							this.word_freq_address = word_freq_address;
							this.word_freq_all = word_freq_all;
							this.word_freq_3d = word_freq_3d;
							this.word_freq_our = word_freq_our;
							this.word_freq_over = word_freq_over;
							this.word_freq_remove = word_freq_remove;
							this.word_freq_internet = word_freq_internet;
							this.word_freq_order = word_freq_order;
							this.word_freq_mail = word_freq_mail;
							this.word_freq_receive = word_freq_receive;
							this.word_freq_will = word_freq_will;
							this.word_freq_people = word_freq_people;
							this.word_freq_report = word_freq_report;
							this.word_freq_addresses = word_freq_addresses;
							this.word_freq_free = word_freq_free;
							this.word_freq_business = word_freq_business;
							this.word_freq_email = word_freq_email;
							this.word_freq_you = word_freq_you;
							this.word_freq_credit = word_freq_credit;
							this.word_freq_your = word_freq_your;
							this.word_freq_font = word_freq_font;
							this.word_freq_000 = word_freq_000;
							this.word_freq_money = word_freq_money;
							this.word_freq_hp = word_freq_hp;
							this.word_freq_hpl = word_freq_hpl;
							this.word_freq_george = word_freq_george;
							this.word_freq_650 = word_freq_650;
							this.word_freq_lab = word_freq_lab;
							this.word_freq_labs = word_freq_labs;
							this.word_freq_telnet = word_freq_telnet;
							this.word_freq_857 = word_freq_857;
							this.word_freq_data = word_freq_data;
							this.word_freq_415 = word_freq_415;
							this.word_freq_85 = word_freq_85;
							this.word_freq_technology = word_freq_technology;
							this.word_freq_1999 = word_freq_1999;
							this.word_freq_parts = word_freq_parts;
							this.word_freq_pm = word_freq_pm;
							this.word_freq_direct = word_freq_direct;
							this.word_freq_cs = word_freq_cs;
							this.word_freq_meeting = word_freq_meeting;
							this.word_freq_original = word_freq_original;
							this.word_freq_project = word_freq_project;
							this.word_freq_re = word_freq_re;
							this.word_freq_edu = word_freq_edu;
							this.word_freq_table = word_freq_table;
							this.word_freq_conference = word_freq_conference;
							this.char_freq_semicolon = char_freq_semicolon;
							this.char_freq_openparenthases = char_freq_openparenthases;
							this.char_freq_openbracket = char_freq_openbracket;
							this.char_freq_exclamation = char_freq_exclamation;
							this.char_freq_dollarsign = char_freq_dollarsign;
							this.char_freq_pound = char_freq_pound;
							this.capital_run_length_average = capital_run_length_average;
							this.capital_run_length_longest = capital_run_length_longest;
							this.capital_run_length_total = capital_run_length_total;
							this.is_spam = is_spam;
						}
        }

        public static void TestOmnitree58()
        {
            //OmnitreePoints<Record, double, double, double, double, double,
            //    double, double, double, double, double, double, double, double,
            //    double, double, double, double, double, double, double, double,
            //    double, double, double, double, double, double, double, double,
            //    double, double, double, double, double, double, double, double,
            //    double, double, double, double, double, double, double, double,
            //    double, double, double, double, double, double, double, double,
            //    double, double, double, double, bool>
            //    omnitree = new OmnitreePointsLinked<Record, double, double, double,
            //        double, double, double, double, double, double, double, double,
            //        double, double, double, double, double, double, double, double,
            //        double, double, double, double, double, double, double, double,
            //        double, double, double, double, double, double, double, double,
            //        double, double, double, double, double, double, double, double,
            //        double, double, double, double, double, double, double, double,
            //        double, double, double, double, double, double, bool>(
            //        (Record record, out double word_freq_make, out double word_freq_address,
            //            out double word_freq_all, out double word_freq_3d, out double word_freq_our,
            //            out double word_freq_over, out double word_freq_remove, out double word_freq_internet,
            //            out double word_freq_order, out double word_freq_mail, out double word_freq_receive, 
            //            out double word_freq_will, out double word_freq_people, out double word_freq_report,
            //            out double word_freq_addresses, out double word_freq_free, out double word_freq_business,
            //            out double word_freq_email, out double word_freq_you, out double word_freq_credit,
            //            out double word_freq_your, out double word_freq_font, out double word_freq_000,
            //            out double word_freq_money, out double word_freq_hp, out double word_freq_hpl,
            //            out double word_freq_george, out double word_freq_650, out double word_freq_lab,
            //            out double word_freq_labs, out double word_freq_telnet, out double word_freq_857,
            //            out double word_freq_data, out double word_freq_415, out double word_freq_85,
            //            out double word_freq_technology, out double word_freq_1999, out double word_freq_parts,
            //            out double word_freq_pm, out double word_freq_direct, out double word_freq_cs,
            //            out double word_freq_meeting, out double word_freq_original, out double word_freq_project,
            //            out double word_freq_re, out double word_freq_edu, out double word_freq_table,
            //            out double word_freq_conference, out double char_freq_semicolon, out double char_freq_openparenthases,
            //            out double char_freq_openbracket, out double char_freq_exclamation, out double char_freq_dollarsign,
            //            out double char_freq_pound, out double capital_run_length_average, out double capital_run_length_longest,
            //            out double capital_run_length_total, out bool is_spam) => { 
            //                word_freq_make = record.word_freq_make; word_freq_address = record.word_freq_address;
            //                word_freq_all = record.word_freq_all; word_freq_3d = record.word_freq_3d;
            //                word_freq_our = record.word_freq_our; word_freq_over = record.word_freq_over;
            //                word_freq_remove = record.word_freq_remove; word_freq_internet = record.word_freq_internet;
            //                word_freq_order = record.word_freq_order; word_freq_mail = record.word_freq_mail;
            //                word_freq_receive = record.word_freq_receive; word_freq_will = record.word_freq_will;
            //                word_freq_people = record.word_freq_people; word_freq_report = record.word_freq_report;
            //                word_freq_addresses = record.word_freq_addresses; word_freq_free = record.word_freq_free;
            //                word_freq_business = record.word_freq_business; word_freq_email = record.word_freq_email;
            //                word_freq_you = record.word_freq_you; word_freq_credit = record.word_freq_credit;
            //                word_freq_your = record.word_freq_your; word_freq_font = record.word_freq_font;
            //                word_freq_000 = record.word_freq_000; word_freq_money = record.word_freq_money;
            //                word_freq_hp = record.word_freq_hp; word_freq_hpl = record.word_freq_hpl;
            //                word_freq_george = record.word_freq_george; word_freq_650 = record.word_freq_650;
            //                word_freq_lab = record.word_freq_lab; word_freq_labs = record.word_freq_labs;
            //                word_freq_telnet = record.word_freq_telnet; word_freq_857 = record.word_freq_857;
            //                word_freq_data = record.word_freq_data; word_freq_415 = record.word_freq_415;
            //                word_freq_85 = record.word_freq_85; word_freq_technology = record.word_freq_technology;
            //                word_freq_1999 = record.word_freq_1999; word_freq_parts = record.word_freq_parts;
            //                word_freq_pm = record.word_freq_pm; word_freq_direct = record.word_freq_direct;
            //                word_freq_cs = record.word_freq_cs; word_freq_meeting = record.word_freq_meeting;
            //                word_freq_original = record.word_freq_original; word_freq_project = record.word_freq_project;
            //                word_freq_re = record.word_freq_re; word_freq_edu = record.word_freq_edu;
            //                word_freq_table = record.word_freq_table; word_freq_conference = record.word_freq_conference;
            //                char_freq_semicolon = record.char_freq_semicolon; char_freq_openparenthases = record.char_freq_openparenthases;
            //                char_freq_openbracket = record.char_freq_openbracket; char_freq_exclamation = record.char_freq_exclamation;
            //                char_freq_dollarsign = record.char_freq_dollarsign; char_freq_pound = record.char_freq_pound;
            //                capital_run_length_average = record.capital_run_length_average; capital_run_length_longest = record.capital_run_length_longest;
            //                capital_run_length_total = record.capital_run_length_total; is_spam = record.is_spam;
            //            });

            //System.Collections.Generic.List<Record> records = new System.Collections.Generic.List<Record>();
            //using (StreamReader reader = new StreamReader(@"C:\Users\Zachary.Patten\Downloads\spambase.arff"))
            //{
            //    while (!reader.ReadLine().Equals("@DATA"))
            //        continue;

            //    while (!reader.EndOfStream)
            //    {

            //        string line = reader.ReadLine().Trim();
            //        if (string.IsNullOrWhiteSpace(line))
            //            continue;
            //        string[] splits = line.Split(',');
            //        if (splits.Length != 58)
            //        {
            //            Console.WriteLine("FUCK");
            //            Console.ReadLine();
            //        }
            //        records.Add(new Record(
            //            double.Parse(splits[0]),
            //            double.Parse(splits[1]), double.Parse(splits[2]), double.Parse(splits[3]), double.Parse(splits[4]), double.Parse(splits[5]),
            //            double.Parse(splits[6]), double.Parse(splits[7]), double.Parse(splits[8]), double.Parse(splits[9]), double.Parse(splits[10]),
            //            double.Parse(splits[11]), double.Parse(splits[12]), double.Parse(splits[13]), double.Parse(splits[14]), double.Parse(splits[15]),
            //            double.Parse(splits[16]), double.Parse(splits[17]), double.Parse(splits[18]), double.Parse(splits[19]), double.Parse(splits[20]),
            //            double.Parse(splits[21]), double.Parse(splits[22]), double.Parse(splits[23]), double.Parse(splits[24]), double.Parse(splits[25]),
            //            double.Parse(splits[26]), double.Parse(splits[27]), double.Parse(splits[28]), double.Parse(splits[29]), double.Parse(splits[30]),
            //            double.Parse(splits[31]), double.Parse(splits[32]), double.Parse(splits[33]), double.Parse(splits[34]), double.Parse(splits[35]),
            //            double.Parse(splits[36]), double.Parse(splits[37]), double.Parse(splits[38]), double.Parse(splits[39]), double.Parse(splits[40]),
            //            double.Parse(splits[41]), double.Parse(splits[42]), double.Parse(splits[43]), double.Parse(splits[44]), double.Parse(splits[45]),
            //            double.Parse(splits[46]), double.Parse(splits[47]), double.Parse(splits[48]), double.Parse(splits[49]), double.Parse(splits[50]),
            //            double.Parse(splits[51]), double.Parse(splits[52]), double.Parse(splits[53]), double.Parse(splits[54]), double.Parse(splits[55]),
            //            double.Parse(splits[56]),
            //            splits[56].Equals("0") ? false : true));
            //    }
            //}


            //for (int i = 0; i < records.Count; i++)
            //{
            //        omnitree.Add(records[i]);
            //}


            ////omnitree.CountSubSpace(0, 0,
            ////    Omnitree.Bound.None, Omnitree.Bound.none,
            ////    Omnitree.Bound.None, Omnitree.Bound.none,
            ////    Omnitree.Bound.None, Omnitree.Bound.none,
            ////    Omnitree.Bound.None, Omnitree.Bound.none,
            ////    Omnitree.Bound.None, Omnitree.Bound.none,
            ////    Omnitree.Bound.None, Omnitree.Bound.none,
            ////    Omnitree.Bound.None, Omnitree.Bound.none,
            ////    Omnitree.Bound.None, Omnitree.Bound.none,
            ////    Omnitree.Bound.None, Omnitree.Bound.none,
            ////    Omnitree.Bound.None, Omnitree.Bound.none,
            ////    Omnitree.Bound.None, Omnitree.Bound.none,
            ////    Omnitree.Bound.None, Omnitree.Bound.none,
            ////    Omnitree.Bound.None, Omnitree.Bound.none,
            ////    Omnitree.Bound.None, Omnitree.Bound.none,
            ////    1, 1);

            //Console.WriteLine("Count: " + omnitree.Count);
        }

        #region hide

        //private static void split(
        //    float[] a,
        //    int n,
        //    float x,
        //    ref int i, ref int j)
        //{
        //    //do the left and right scan until the pointers cross
        //    do
        //    {
        //        //scan from the left then scan from the right
        //        while (a[i] < x) i++;
        //        while (x < a[j]) j--;
        //        //now swap values if they are in the wrong part:
        //        if (i <= j)
        //        {
        //            float t = a[i];
        //            a[i] = a[j];
        //            a[j] = t;
        //            i++; j--;
        //        }
        //        //and continue the scan until the pointers cross:
        //    } while (i <= j);
        //}

        //private static void median(
        //    float[] a,
        //    int n,
        //    ref int k)
        //{
        //    int L = 0;
        //    int R = n - 1;
        //    k = n / 2;
        //    int i; int j;
        //    while (L < R)
        //    {
        //        float x = a[k];
        //        i = L; j = R;
        //        split(a, n, x, ref i, ref j);
        //        if (j < k) L = i;
        //        if (k < i) R = j;
        //    }
        //}

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
		
        //public static void TestAngles()
        //{
        //    Console.WriteLine("  Angles--------------------------------------");
        //    Console.WriteLine();
        //    Angle<double> angle1 = Angle<double>.Factory_Degrees(90d);
        //    Console.WriteLine("    angle1 = " + angle1);
        //    Angle<double> angle2 = Angle<double>.Factory_Turns(0.5d);
        //    Console.WriteLine("    angle2 = " + angle2);
        //    Console.WriteLine("    angle1 + angle2 = " + (angle1 + angle2));
        //    Console.WriteLine("    angle2 - angle1 = " + (angle1 + angle2));
        //    Console.WriteLine("    angle1 * 2 = " + (angle1 * 2));
        //    Console.WriteLine("    angle1 / 2 = " + (angle1 / 2));
        //    Console.WriteLine("    angle1 > angle2 = " + (angle1 > angle2));
        //    Console.WriteLine("    angle1 == angle2 = " + (angle1 == angle2));
        //    Console.WriteLine("    angle1 * 2 == angle2 = " + (angle1 * 2 == angle2));
        //    Console.WriteLine("    angle1 != angle2 = " + (angle1 != angle2));
        //    Console.WriteLine();
        //}

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

        public static void TestOmnitreeGiant(bool multithread)
        {
            ulong count = 100000;

            bool query_single = true;

            Console.WriteLine("Testing with " + count + " records...");
            if (multithread)
                Console.WriteLine("multithreading");
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
