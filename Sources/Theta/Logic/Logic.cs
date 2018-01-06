//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Theta.Logic
//{
//	public class Logic<T>
//	{

//		#region Definition

//		public abstract class Node
//		{
//			public static implicit operator Node(T constant) { return new Constant(constant); }

//			public Node Simplify() { return Symbolics<T>.Simplify(this); }
//			public Node Assign(string variable, T value) { return Symbolics<T>.Assign(this, variable, value); }
//			public Node Derive(string variable) { return Symbolics<T>.Derive(this, variable); }
//			public Node Integrate(string variable) { return Symbolics<T>.Integrate(this, variable); }
//		}

//		public class Constant : Node
//		{
//			T _constant;

//			public T Value { get { return this._constant; } }

//			public Constant(T constant)
//			{
//				this._constant = constant;
//			}

//			public static implicit operator T(Constant constant) { return constant._constant; }

//			public override string ToString() { return this._constant.ToString(); }
//		}

//		public class Variable : Node
//		{
//			public string _name;

//			public string Name { get { return this._name; } }

//			public Variable(string name)
//			{
//				this._name = name;
//			}

//			public override string ToString() { return this._name; }
//		}

//		public abstract class Operation : Node
//		{
//		}

//		public abstract class Unary : Operation
//		{
//			protected Node _operand;

//			public Node Operand
//			{
//				get { return this._operand; }
//				set { this._operand = value; }
//			}

//			public Unary() : base() { }

//			public Unary(Node operand)
//			{
//				this._operand = operand;
//			}
//		}

//		public abstract class Binary : Operation
//		{
//			protected Node _left;
//			protected Node _right;

//			public Node Left
//			{
//				get { return this._left; }
//				set { this._left = value; }
//			}

//			public Node Right
//			{
//				get { return this._right; }
//				set { this._right = value; }
//			}

//			public Binary() { }

//			public Binary(Node left, Node right)
//			{
//				this._left = left;
//				this._right = right;
//			}
//		}

//		public abstract class Ternary : Operation
//		{
//			protected Node _one;
//			protected Node _two;
//			protected Node _three;

//			public Node One
//			{
//				get { return this._one; }
//				set { this._one = value; }
//			}

//			public Node Two
//			{
//				get { return this._two; }
//				set { this._two = value; }
//			}

//			public Node Three
//			{
//				get { return this._three; }
//				set { this._three = value; }
//			}

//			public Ternary() { }

//			public Ternary(Node one, Node two, Node three)
//			{
//				this._one = one;
//				this._two = two;
//				this._three = three;
//			}
//		}

//		public abstract class Multinary : Operation
//		{
//			protected Node[] _operands;

//			public Node[] Operands
//			{
//				get { return this._operands; }
//				set { this._operands = value; }
//			}

//			public Multinary() { }

//			public Multinary(Node[] operands)
//			{
//				this._operands = operands;
//			}
//		}

//		#endregion




//		public const string material_implication = "⇒";
//		public const string double_material_implication = "⇔";
//		public const string negation = "¬";
//		public const string logical_conjunction = "∧";
//		public const string inclusive_logical_disjunction = "∨";
//		public const string exclusive_disjunction = "⊕";
//		public const string tautology = "⊤";
//		public const string contradiction = "⊥";
//		public const string universal_quantification = "∀";
//		public const string existential_quantification = "∃";
//		public const string uniqueness_quantification = "∃!";
//		public const string definition = ":=";
//		public const string precedence_grouping_preceding = "(";
//		public const string precedence_grouping_proceeding = "(";
//		public const string turnstile = "⊢";
//		public const string double_turnstile = "⊨";
//		public const string negated_logical_conjunction = "⊼";
//		public const string negated_inclusive_logical_disjunction = "⊽";

//		public class material_implication
//		{
//			T _left;
//			T _right;

//			public T Left { get { return this._left; } }
//			public T Right { get { return this._right; } }

//			private material_implication(T left, T right)
//			{
//				this._left = left;
//				this._right = right;
//			}
//		}

//		public class double_material_implication
//		{
//			T _left;
//			T _right;

//			public T Left { get { return this._left; } }
//			public T Right { get { return this._right; } }

//			private double_material_implication(T left, T right)
//			{
//				this._left = left;
//				this._right = right;
//			}
//		}

//		public class negation
//		{
//			T _operand;

//			public T Left { get { return this._operand; } }

//			private negation(T operand)
//			{
//				this._operand = operand;
//			}
//		}

//		public class logical_conjunction
//		{
//			T _left;
//			T _right;

//			public T Left { get { return this._left; } }
//			public T Right { get { return this._right; } }

//			private logical_conjunction(T left, T right)
//			{
//				this._left = left;
//				this._right = right;
//			}
//		}

//		public class inclusive_logical_disjunction
//		{
//			T _left;
//			T _right;

//			public T Left { get { return this._left; } }
//			public T Right { get { return this._right; } }

//			private inclusive_logical_disjunction(T left, T right)
//			{
//				this._left = left;
//				this._right = right;
//			}
//		}

//		public class exclusive_disjunction
//		{
//			T _left;
//			T _right;

//			public T Left { get { return this._left; } }
//			public T Right { get { return this._right; } }

//			private exclusive_disjunction(T left, T right)
//			{
//				this._left = left;
//				this._right = right;
//			}
//		}

//		public class tautology
//		{
//			T _left;

//			public T Left { get { return this._left; } }

//			private tautology(T left, T right)
//			{
//				this._left = left;
//			}
//		}

//		public class contradiction
//		{
//			T _left;

//			public T Left { get { return this._left; } }

//			private contradiction(T left, T right)
//			{
//				this._left = left;
//			}
//		}

//		public class universal_quantification
//		{

//		}

//		public class existential_quantification
//		{
//		}

//		public class uniqueness_quantification
//		{
//		}

//		public class definition
//		{
//		}

//		public class precedence_grouping_preceding
//		{
//		}

//		public class precedence_grouping_proceeding
//		{
//		}

//		public class turnstile
//		{
//		}

//		public class double_turnstile
//		{
//		}

//		public class negated_logical_conjunction
//		{
//		}

//		public class negated_inclusive_logical_disjunction
//		{
//		}

//	}
//}
