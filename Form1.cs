using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        string Link = "https://www.imdb.com/chart/top/?ref_=nv_mv_250";
        string Xpath = "/html/body/div[3]/div/div[2]/div[3]/div/div[1]/div/span/div/div/div[3]/table/tbody/tr[position()>0]";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadMovies();
        }

        void LoadMovies()
        {
            var Movies = new List<Movie>();

            var Web = new HtmlWeb();

            var Dock = Web.Load(Link);

            var Nodes = Dock.DocumentNode.SelectNodes(Xpath);

            foreach (var Node in Nodes)
            {
                try
                {
                    var Movie = new Movie
                    {
                        Name = Node.SelectSingleNode("td[2]/a").InnerText,
                        Rating = Node.SelectSingleNode("td[3]/strong").InnerText,
                    };

                    Movies.Add(Movie);
                }
                catch { }
            }

            foreach (var Movie in Movies)
            {
                dataGridView1.Rows.Add(Movie.Name, Movie.Rating);
            }

        }
        class Movie
        {
            public string Name { get; set; }
            public string Rating { get; set; }
        }

    }
}
