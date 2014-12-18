﻿using System;

namespace KerbalCalc
{
    internal class Calc
    {
        public string Screen { get; set; }
        private Func<double, double, double> _operation;
        private double _heldOperand;

        public Calc()
        {
            Screen = String.Empty;
        }

        public void AddDigit(int digit)
        {
            if (Screen.Contains("ERROR"))
                Screen = "";

            Screen += digit.ToString();
        }

        public void AddDecimal()
        {
            if (Screen.Contains("ERROR"))
                Screen = "";

            if (Screen.Contains("."))
                return;

            if (String.IsNullOrEmpty(Screen))
                Screen = "0";

            Screen += ".";
        }

        public void NaturalLog()
        {
            double operand;

            if (double.TryParse(Screen, out operand))
                Screen = Math.Log(operand).ToString();
            else
                Screen = "ERROR";
        }

        public void Calculate()
        {
            double operand;

            if (double.TryParse(Screen, out operand) && _operation != null)
                Screen = _operation(_heldOperand, operand).ToString();
            else
                Screen = "ERROR";
        }

        public void StageAdd()
        {
            StageOperator((firstOperand, secondOperand) => firstOperand + secondOperand);
        }

        public void StageSubtract()
        {
            StageOperator((firstOperand, secondOperand) => firstOperand - secondOperand);
        }

        public void StageMultiply()
        {
            StageOperator((firstOperand, secondOperand) => firstOperand * secondOperand);
        }

        public void StageDivide()
        {
            StageOperator((firstOperand, secondOperand) => firstOperand / secondOperand);
        }

        private void StageOperator(Func<double, double, double> operation)
        {
            if (!double.TryParse(Screen, out _heldOperand))
            {
                Screen = "ERROR";
                return;
            }

            _operation = operation;
            Screen = "";
        }
    }
}