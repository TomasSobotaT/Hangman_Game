using Obesenec;

namespace Obesenec_hra
{
    public partial class HraForm : Form
    {
        public Hra hra;

        public HraForm()
        {
            InitializeComponent();
            uvodniPictureBox.Image = obrazkyList.Images[11];
        }

        /// <summary>
        /// Metoda ukonèující program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void konecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Chcete opravdu skonèit?", "Ukonèit program", MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
                Environment.Exit(-1);
        }

        /// <summary>
        /// metoda zahajující hru od zaèátku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void novaHraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hledaneSlovoLabel.Text = "";
            finalniObrazovkaPanel.Visible = false;
            hlavniHraPanel.Visible = true;
            uvodniObrazovkaPanel.Visible = false;
            obrazkyPictureBox.Image = obrazkyList.Images[0];

            try
            {
                hra = new Hra();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Nastala chyba pøi ètení slov ze souboru. Chyba: " + ex.Message);
            }
           

            hledaneSlovoLabel.Text = hra.NovaHra();

            //zvyditelní všechny buttony na panelu hry
            foreach (Control item in hlavniHraPanel.Controls)
            {
                if (item is Button)
                    item.Visible = true;
            }


        }


        /// <summary>
        /// stisknutí vybraného písmene
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void vyberPismeno(object sender, EventArgs e)
        {
            string vybranePismeno = (sender as Button).Text.ToString();
            (sender as Button).Visible = false;

            if (hra.score < 9)
            {
                string slovo = hra.Zkontroluj(vybranePismeno, hledaneSlovoLabel.Text);
                hledaneSlovoLabel.Text = slovo;

                if (slovo.Replace(" ","") == hra.HledaneSlovo)
                    konecHry(true);

                obrazkyPictureBox.Image = obrazkyList.Images[hra.score];
            }
            else
                konecHry(false);

        }

        /// <summary>
        /// metoda ukonèující hru bud výtìzstvím nebo prohrou
        /// </summary>
        /// <param name="vyhra">true = vyhra, false = prohra</param>
        private void konecHry(bool vyhra)
        {
            finalniObrazovkaPanel.Visible = true;
            hlavniHraPanel.Visible = false;
            uvodniObrazovkaPanel.Visible  = false;

            if (vyhra)
            {
                konecLabel.Text = "Vyhrál jsi. Uhodl jsi slovo: " + hra.HledaneSlovo;
                konecPictureBox.Image = obrazkyList.Images[12];
            }
            else 
            {
                konecLabel.Text = "Prohrál jsi. Hledané slovo: " + hra.HledaneSlovo;
                konecPictureBox.Image = obrazkyList.Images[10];
            }

        }

      
    }
}