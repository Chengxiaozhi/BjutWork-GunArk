using System;
using System.Collections.Generic;
using System.Text;

namespace MyUI.Utils
{
    public class PrinterInstance
    {
        private static PrinterInstance instance;
        private Printer printer;

        private PrinterInstance()
        {
            printer = new Printer();
        }

        public static PrinterInstance getInstance()
        {
            if (instance == null)
            {
                instance = new PrinterInstance();
                return instance;
            }

            return instance;
        }

        public Printer getPrinter()
        {
            return this.printer;
        }
    }
}
