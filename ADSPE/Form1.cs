using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADSPE
{
    public partial class Form1 : Form
    {
        List<Chromosome> initialPopulation = new List<Chromosome>();
        public Form1()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {   
            int populationSize = Convert.ToInt32(populationTextBox.Text);
            string benchmarkPath = @"Traces/" + benchmarkComboBox.Text;
            int generations = Convert.ToInt32(generationsTextBox.Text);
            double crossover = Convert.ToDouble(crossoverComboBox.Text);
            string selectionOperator = selectionComboBox.Text;

            GeneticAlgorithm spea2 = new GeneticAlgorithm(populationSize, generations, crossover, benchmarkPath, selectionOperator);
            spea2.Start();

            MessageBox.Show("Finished running all simulations. Check FUN and VAR files for results");
        }
    }
}
