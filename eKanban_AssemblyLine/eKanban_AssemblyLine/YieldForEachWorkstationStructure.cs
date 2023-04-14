/*
* FILE			: YieldForEachWorkstationStructure.cs
* PROJECT		: PROG3070 - Project
* PROGRAMMER	: Enes Demirsoz, Jessica Sim, Hoda Akrami
* FIRST VERSION : 2022-11-26
* DESCRIPTION	: This file contains YieldForEachWorkstationStructure class which defines the structure of yield for each workstation
*/

using System.Windows.Forms;

namespace eKanban_AssemblyLine
{
    public class YieldForEachWorkstationStructure
    {
        public int ID { get; set; }
        public Label WorkstationName { get; set; }
        public Label Yield = new Label();
        public int Row { get; set; }
    }
}
