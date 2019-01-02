using System;

//https://www.codewars.com/kata/fluent-calculator-1
namespace CODEWARS.Kata4_FluentCalculator
{
    public class FluentCalculator
    {
        bool valueSet = false;
        double? result = null;
        double? value1 = null;
        char? op1 = null;
        char? op2 = null;

        public FluentCalculator Zero => this.ChangeValue(0);
        public FluentCalculator One => this.ChangeValue(1);
        public FluentCalculator Two => this.ChangeValue(2);
        public FluentCalculator Three => this.ChangeValue(3);
        public FluentCalculator Four => this.ChangeValue(4);
        public FluentCalculator Five => this.ChangeValue(5);
        public FluentCalculator Six => this.ChangeValue(6);
        public FluentCalculator Seven => this.ChangeValue(7);
        public FluentCalculator Eight => this.ChangeValue(8);
        public FluentCalculator Nine => this.ChangeValue(9);
        public FluentCalculator Ten => this.ChangeValue(10);

        public FluentCalculator Plus => this.SetOperation('+');
        public FluentCalculator Minus => this.SetOperation('-');
        public FluentCalculator Times => this.SetOperation('*');
        public FluentCalculator DividedBy => this.SetOperation('/');

        public double Result() => this.GetResult();
        public static implicit operator double (FluentCalculator x) => x.GetResult();

        private FluentCalculator SetOperation(char op)
        {
            valueSet = false;

            if (this.op1 == null)
            {
                this.op1 = op;
            }
            else if (op2 == null)
            {
                this.op2 = op;
            }
            else
            {
                throw new InvalidOperationException();
            }

            return this;
        }

        private double GetResult()
        {
            if (this.value1.HasValue)
            {
                return this.DoOperation(this.result.Value, this.value1.Value, this.op1.Value);
            }

            return this.result.Value;
        }

        private double DoOperation(double val1, double val2, char op)
        {
            switch (op)
            {
                case '+': return val1 + val2;
                case '-': return val1 - val2;
                case '*': return val1 * val2;
                case '/': return val1 / val2;
                default: throw new InvalidOperationException();
            }
        }

        private void ResetValues()
        {
            this.valueSet = false;
            this.result = null;
            this.value1 = null;
            this.op1 = null;
            this.op2 = null;
        }

        private FluentCalculator ChangeValue(int value)
        {
            if (this.valueSet)
            {
                this.ResetValues();
            }

            this.valueSet = true;

            if (this.result == null)
            {
                this.result = value;
            }
            else if (this.value1 == null)
            {
                if (this.op1 == '*' || this.op1 == '/')
                {
                    this.result = this.DoOperation(this.result.Value, value, this.op1.Value);
                    this.op1 = null;
                }
                else
                {
                    this.value1 = value;
                }
            }
            else
            {
                if (this.op2 == '+' || this.op2 == '-')
                {
                    this.result = this.DoOperation(this.result.Value, this.value1.Value, this.op1.Value);
                    this.value1 = value;
                    this.op1 = this.op2;
                }
                else
                {
                    this.value1 = this.DoOperation(this.value1.Value, value, this.op2.Value);
                }

                this.op2 = null;
            }

            return this;
        }
    }
}