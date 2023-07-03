using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.Structure;
using System.Timers;

namespace RobotRysujacyLinie
{
    public partial class OknoGlowne : Form
    {
        private Size wymiarOkna;

        private Image<Rgb, byte> imageBoisko;

        private Image<Rgb, byte> imageBoiskoZRobotem;

        public int bateria = 100;
        public int krok = 0;
        public int krokPomoc = 0;
        public int ktoryEtap = 0;

        public struct Etap
        {
            public char czyMalowane;
            public string liniaCzyLuk;
            public int ileKrokow;
            public int szerokoscLinii;
            public int Xpocz;
            public int Ypocz;
            public int Xkon;
            public int Ykon;

            public int Xcen;
            public int Ycen;
            public int promien;
            public int odKat;
            public int doKat;

            public void setEtapLinia(char czyMalowane, int ileKrokow, int szerokoscLinii, int Xpocz, int Ypocz, int Xkon, int Ykon)
            {
                this.czyMalowane = czyMalowane;
                liniaCzyLuk = "linia";
                if (ileKrokow==0)
                {
                    if (Math.Abs(Xkon - Xpocz) > Math.Abs(Ykon - Ypocz)) this.ileKrokow = Math.Abs(Xkon - Xpocz);
                    else this.ileKrokow = Math.Abs(Ykon - Ypocz);
                }
                else this.ileKrokow = ileKrokow;
                this.szerokoscLinii = szerokoscLinii;
                this.Xpocz = Xpocz;
                this.Ypocz = Ypocz;
                if (Math.Abs(Xkon) == 1) this.Xkon = Xpocz + Xkon * ileKrokow;
                else this.Xkon = Xkon;
                if (Math.Abs(Ykon) == 1) this.Ykon = Ypocz + Ykon * ileKrokow;
                else this.Ykon = Ykon;
            }

            public void setEtapLinia(char czyMalowane, int Xpocz, int Ypocz, int Xkon, int Ykon)
            {
                this.czyMalowane = czyMalowane;
                liniaCzyLuk = "linia";

                if (Math.Abs(Xkon - Xpocz) > Math.Abs(Ykon - Ypocz)) this.ileKrokow = Math.Abs(Xkon - Xpocz);
                else this.ileKrokow = Math.Abs(Ykon - Ypocz);
                
                this.szerokoscLinii = 10;
                this.Xpocz = Xpocz;
                this.Ypocz = Ypocz;
                this.Xkon = Xkon;
                this.Ykon = Ykon;
            }

            public void setEtapLuk(int ileKrokow, int Xcen, int Ycen, int promien, int odKat, int doKat, int szerokoscLinii)
            {
                czyMalowane = 'm';
                liniaCzyLuk = "luk";
                this.ileKrokow = ileKrokow;
                this.Xcen = Xcen;
                this.Ycen = Ycen;
                this.promien = promien;
                this.odKat = odKat;
                this.doKat = doKat;
                this.szerokoscLinii = szerokoscLinii;

                Xkon = (int)( Math.Sin( ( (double)doKat * Math.PI ) / 180 ) * promien + Xcen );
                Ykon = (int)( Math.Cos( ( (double)doKat * Math.PI ) / 180 ) * promien + Ycen );
            }

            public void setEtapLuk(int Xcen, int Ycen, int promien, int odKat, int doKat)
            {
                czyMalowane = 'm';
                liniaCzyLuk = "luk";
                this.ileKrokow = 180;
                this.Xcen = Xcen;
                this.Ycen = Ycen;
                this.promien = promien;
                this.odKat = odKat;
                this.doKat = doKat;
                this.szerokoscLinii = 10;

                Xkon = (int)(Math.Sin(((double)doKat * Math.PI) / 180) * promien + Xcen);
                Ykon = (int)(Math.Cos(((double)doKat * Math.PI) / 180) * promien + Ycen);
            }

        }

        public Etap[] etapyPilkaNozna;
        //public List<Etap> etapyPilkaNozna;

        public OknoGlowne()
        {
            InitializeComponent();

            wymiarOkna = new Size(466, 657);

            imageBoisko = new Image<Rgb, byte>(wymiarOkna);

            imageBoiskoZRobotem = new Image<Rgb, byte>(wymiarOkna);

            //pictureBoxBoisko.Image = imageBoisko.Bitmap;

            //Rysuj rysuj = new Rysuj();
            //rysuj.BoiskoPilkaNozna(imageBoisko);


            comboBoxTrybPracy.Items.Insert(0, "Rysowanie");
            comboBoxTrybPracy.Items.Insert(1, "Serwis");

            comboBoxTypBoiska.Items.Insert(0, "Piłka nożna");
            comboBoxTypBoiska.Items.Insert(1, "Koszykówka");

            comboBoxRysowanie.Items.Insert(0, "Powielanie linii");
            comboBoxRysowanie.Items.Insert(1, "Nowe linie");

            progressBar1.Maximum = 6695; //boisko pilka nozna 6695 krokow

            labelBateria.Text = bateria + "%";

            etapyPilkaNozna = new Etap[52];
            //etapyPilkaNozna.Add(pojedynczyEtap);

            etapyPilkaNozna[0].setEtapLinia('m',wymiarOkna.Height - 40, 10, 25, wymiarOkna.Height - 20, 25, -1); //lewa w gore
            etapyPilkaNozna[1].setEtapLinia('m', 30, 10, etapyPilkaNozna[0].Xkon-5, etapyPilkaNozna[0].Ykon+5, 1, etapyPilkaNozna[0].Ykon+5); //gorna w prawo
            etapyPilkaNozna[2].setEtapLuk(25, 25, 30, 90, 0); //lewy gorny luk
            etapyPilkaNozna[3].setEtapLinia('m', etapyPilkaNozna[2].Xkon, etapyPilkaNozna[2].Ykon, 25, 25); //gora
            etapyPilkaNozna[4].setEtapLinia('m', etapyPilkaNozna[3].Xkon, etapyPilkaNozna[3].Ykon, 120, 25); //prawo
            etapyPilkaNozna[5].setEtapLinia('m', etapyPilkaNozna[4].Xkon, etapyPilkaNozna[4].Ykon, 120, 105); //dol
            etapyPilkaNozna[6].setEtapLinia('m', 114, 100, 277, 100); //w prawo do luku
            etapyPilkaNozna[7].setEtapLuk(233, 80, 50, 68, -68); //luk
            etapyPilkaNozna[8].setEtapLinia('n', etapyPilkaNozna[7].Xkon, etapyPilkaNozna[7].Ykon, wymiarOkna.Width / 2, 80); //dojscie do kropki
            etapyPilkaNozna[9].setEtapLuk(etapyPilkaNozna[8].Xkon, etapyPilkaNozna[8].Ykon, 5, 0, 360); //malowanie kropki
            etapyPilkaNozna[10].setEtapLinia('n', etapyPilkaNozna[8].Xkon, etapyPilkaNozna[8].Ykon, etapyPilkaNozna[6].Xkon, etapyPilkaNozna[6].Ykon); //powrot na linie
            etapyPilkaNozna[11].setEtapLinia('m', etapyPilkaNozna[10].Xkon, 100, 350, 100); //linia w prawo
            etapyPilkaNozna[12].setEtapLinia('m', wymiarOkna.Width - 120, 105, wymiarOkna.Width - 120, 20);
            etapyPilkaNozna[13].setEtapLinia('m', etapyPilkaNozna[12].Xkon, 25, 120, 24);
            etapyPilkaNozna[14].setEtapLinia('n', etapyPilkaNozna[13].Xkon, etapyPilkaNozna[13].Ykon, 185, 25);
            etapyPilkaNozna[15].setEtapLinia('m', etapyPilkaNozna[14].Xkon, etapyPilkaNozna[14].Ykon, 185, 54);
            etapyPilkaNozna[16].setEtapLinia('m', 180, 50, 286, 50);
            etapyPilkaNozna[17].setEtapLinia('m', 282, 54, 282, 25);
            etapyPilkaNozna[18].setEtapLinia('m', etapyPilkaNozna[17].Xkon, etapyPilkaNozna[17].Ykon, wymiarOkna.Width - 20, 25);
            etapyPilkaNozna[19].setEtapLinia('m', wymiarOkna.Width - 25, 20, wymiarOkna.Width - 25, 50);
            etapyPilkaNozna[20].setEtapLuk(wymiarOkna.Width - 25, 25, 30, 0, -90);
            etapyPilkaNozna[21].setEtapLinia('n', etapyPilkaNozna[20].Xkon, etapyPilkaNozna[20].Ykon, wymiarOkna.Width - 25, 25);
            etapyPilkaNozna[22].setEtapLinia('m', etapyPilkaNozna[21].Xkon, etapyPilkaNozna[21].Ykon, wymiarOkna.Width - 25, wymiarOkna.Height / 2);
            etapyPilkaNozna[23].setEtapLinia('m', etapyPilkaNozna[22].Xkon, etapyPilkaNozna[22].Ykon, wymiarOkna.Width / 2, wymiarOkna.Height / 2);
            etapyPilkaNozna[24].setEtapLuk(etapyPilkaNozna[23].Xkon, etapyPilkaNozna[23].Ykon, 5, 0, 360);
            etapyPilkaNozna[25].setEtapLinia('n', etapyPilkaNozna[24].Xkon, etapyPilkaNozna[24].Ykon, wymiarOkna.Width / 2, wymiarOkna.Height / 2);
            etapyPilkaNozna[26].setEtapLinia('m', etapyPilkaNozna[25].Xkon, etapyPilkaNozna[25].Ykon, 150, wymiarOkna.Height/2);
            etapyPilkaNozna[27].setEtapLuk(wymiarOkna.Width / 2, wymiarOkna.Height / 2, 80, -90, -270);
            etapyPilkaNozna[28].setEtapLuk(wymiarOkna.Width / 2, wymiarOkna.Height / 2, 80, 90, -90);
            etapyPilkaNozna[29].setEtapLinia('m', etapyPilkaNozna[28].Xkon, etapyPilkaNozna[28].Ykon, 20, wymiarOkna.Height / 2);
            etapyPilkaNozna[30].setEtapLinia('n', etapyPilkaNozna[29].Xkon, etapyPilkaNozna[29].Ykon, wymiarOkna.Width - 20, wymiarOkna.Height / 2);
            etapyPilkaNozna[31].setEtapLinia('m', wymiarOkna.Width - 25, wymiarOkna.Height / 2, wymiarOkna.Width - 25, wymiarOkna.Height - 20);
            etapyPilkaNozna[32].setEtapLinia('m', wymiarOkna.Width - 20, wymiarOkna.Height - 25, wymiarOkna.Width - 25 - 30, wymiarOkna.Height - 25);
            etapyPilkaNozna[33].setEtapLuk(wymiarOkna.Width - 25, wymiarOkna.Height - 25, 30, -90, -180);
            etapyPilkaNozna[34].setEtapLinia('n', etapyPilkaNozna[33].Xkon, etapyPilkaNozna[33].Ykon, wymiarOkna.Width - 25, wymiarOkna.Height - 25);
            etapyPilkaNozna[35].setEtapLinia('m', etapyPilkaNozna[34].Xkon, etapyPilkaNozna[34].Ykon, 120, etapyPilkaNozna[34].Ykon);
            etapyPilkaNozna[36].setEtapLinia('m', etapyPilkaNozna[35].Xkon, etapyPilkaNozna[35].Ykon, etapyPilkaNozna[35].Xkon, 553);
            etapyPilkaNozna[37].setEtapLinia('m', 115, 557, 278, 557);
            etapyPilkaNozna[38].setEtapLuk(233, wymiarOkna.Height - 80, 50, 180-68, 180+68); //luk
            etapyPilkaNozna[39].setEtapLinia('n', etapyPilkaNozna[38].Xkon, etapyPilkaNozna[38].Ykon, wymiarOkna.Width / 2, wymiarOkna.Height - 80);
            etapyPilkaNozna[40].setEtapLuk(etapyPilkaNozna[39].Xkon, etapyPilkaNozna[39].Ykon, 5, 0, 360); //malowanie kropki
            etapyPilkaNozna[41].setEtapLinia('n', etapyPilkaNozna[40].Xkon, etapyPilkaNozna[40].Ykon, 278, 557);
            etapyPilkaNozna[42].setEtapLinia('m', etapyPilkaNozna[41].Xkon, etapyPilkaNozna[41].Ykon, 350, etapyPilkaNozna[41].Ykon);
            etapyPilkaNozna[43].setEtapLinia('m', 345, etapyPilkaNozna[42].Ykon, 345, wymiarOkna.Height - 25);
            etapyPilkaNozna[44].setEtapLinia('m', etapyPilkaNozna[43].Xkon, etapyPilkaNozna[43].Ykon, 282, etapyPilkaNozna[43].Ykon);
            etapyPilkaNozna[45].setEtapLinia('m', etapyPilkaNozna[44].Xkon, etapyPilkaNozna[44].Ykon, 282, 603);
            etapyPilkaNozna[46].setEtapLinia('m', 282, 608, 179, 608);
            etapyPilkaNozna[47].setEtapLinia('m', 184, etapyPilkaNozna[46].Ykon, 184, wymiarOkna.Height - 25);
            etapyPilkaNozna[48].setEtapLinia('m', etapyPilkaNozna[47].Xkon, etapyPilkaNozna[47].Ykon, 25, wymiarOkna.Height - 25);
            etapyPilkaNozna[49].setEtapLinia('n', etapyPilkaNozna[48].Xkon, etapyPilkaNozna[48].Ykon, 25, 602);
            etapyPilkaNozna[50].setEtapLuk(25, wymiarOkna.Height - 25, 30, -180, -270); //lewy dolny luk
            etapyPilkaNozna[51].setEtapLinia('n', etapyPilkaNozna[50].Xkon, etapyPilkaNozna[50].Ykon, 25, wymiarOkna.Height - 25);

        }

        private void comboBoxTrybPracy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTrybPracy.SelectedIndex == 0)//rysowanie
            {
                imageBoisko.SetValue(new MCvScalar(30, 30, 30));
                comboBoxTypBoiska.Enabled = true;
            }
            if (comboBoxTrybPracy.SelectedIndex == 1)//serwis
            {
                imageBoisko.SetValue(new MCvScalar(0, 0, 230));
                comboBoxTypBoiska.Enabled = false;
                comboBoxRysowanie.Enabled = false;
            }

            pictureBoxBoisko.Image = imageBoisko.Bitmap;
        }

        private void comboBoxTypBoiska_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte[,,] temp = imageBoisko.Data;

            if (comboBoxTypBoiska.SelectedIndex == 0)//pilka nozna
            {
                imageBoisko.SetValue(new MCvScalar(0, 150, 0)); //boisko do pilki
                comboBoxRysowanie.Enabled = true;

                //linia lewa
                for (int dlugoscLinii = 0; dlugoscLinii < wymiarOkna.Height - 40; dlugoscLinii++)
                {
                    for (int szerokoscLinii = 0; szerokoscLinii < 10; szerokoscLinii++)
                    {
                        UstawKolor(temp, wymiarOkna.Height - 20 - dlugoscLinii, 20 + szerokoscLinii, 255);
                    }
                }
                //linia gorna
                for (int dlugoscLinii = 0; dlugoscLinii < wymiarOkna.Width - 40; dlugoscLinii++)
                {
                    for (int szerokoscLinii = 0; szerokoscLinii < 10; szerokoscLinii++)
                    {
                        UstawKolor(temp, 20 + szerokoscLinii, 20 + dlugoscLinii, 255);
                    }
                }
                //linia prawa
                for (int dlugoscLinii = 0; dlugoscLinii < wymiarOkna.Height - 40; dlugoscLinii++)
                {
                    for (int szerokoscLinii = 0; szerokoscLinii < 10; szerokoscLinii++)
                    {
                        UstawKolor(temp, 20 + dlugoscLinii, wymiarOkna.Width - 20 - szerokoscLinii, 255);
                    }
                }
                //linia dolna
                for (int dlugoscLinii = 0; dlugoscLinii < wymiarOkna.Width - 40; dlugoscLinii++)
                {
                    for (int szerokoscLinii = 0; szerokoscLinii < 10; szerokoscLinii++)
                    {
                        UstawKolor(temp, wymiarOkna.Height - 20 - szerokoscLinii, wymiarOkna.Width - 20 - dlugoscLinii, 255);
                    }
                }
                //linia srodka
                for (int dlugoscLinii = 0; dlugoscLinii < wymiarOkna.Width - 40; dlugoscLinii++)
                {
                    for (int szerokoscLinii = 0; szerokoscLinii < 10; szerokoscLinii++)
                    {
                        UstawKolor(temp, wymiarOkna.Height / 2 + 5 - szerokoscLinii, 20 + dlugoscLinii, 255);
                    }
                }

                //pole karne gora
                for (int dlugoscLinii = 0; dlugoscLinii < 75; dlugoscLinii++)
                {
                    for (int szerokoscLinii = 0; szerokoscLinii < 10; szerokoscLinii++)
                    {
                        UstawKolor(temp, 20 + dlugoscLinii, wymiarOkna.Width / 4 + szerokoscLinii, 255);
                    }
                }
                for (int dlugoscLinii = 0; dlugoscLinii < wymiarOkna.Width / 2 + 2; dlugoscLinii++)
                {
                    for (int szerokoscLinii = 0; szerokoscLinii < 10; szerokoscLinii++)
                    {
                        UstawKolor(temp, 20 + 75 + szerokoscLinii, wymiarOkna.Width / 4 + dlugoscLinii, 255);
                    }
                }
                for (int dlugoscLinii = 0; dlugoscLinii < 75; dlugoscLinii++)
                {
                    for (int szerokoscLinii = 0; szerokoscLinii < 10; szerokoscLinii++)
                    {
                        UstawKolor(temp, 20 + 74 - dlugoscLinii, wymiarOkna.Width - wymiarOkna.Width / 4 - szerokoscLinii, 255);
                    }
                }

                //pole karne dol
                for (int dlugoscLinii = 0; dlugoscLinii < 75; dlugoscLinii++)
                {
                    for (int szerokoscLinii = 0; szerokoscLinii < 10; szerokoscLinii++)
                    {
                        UstawKolor(temp, wymiarOkna.Height - 20 - dlugoscLinii, wymiarOkna.Width - wymiarOkna.Width / 4 - szerokoscLinii, 255);
                    }
                }
                for (int dlugoscLinii = 0; dlugoscLinii < wymiarOkna.Width / 2 + 2; dlugoscLinii++)
                {
                    for (int szerokoscLinii = 0; szerokoscLinii < 10; szerokoscLinii++)
                    {
                        UstawKolor(temp, wymiarOkna.Height - 20 - 75 - szerokoscLinii, wymiarOkna.Width - wymiarOkna.Width / 4 - dlugoscLinii, 255);
                    }
                }
                for (int dlugoscLinii = 0; dlugoscLinii < 75; dlugoscLinii++)
                {
                    for (int szerokoscLinii = 0; szerokoscLinii < 10; szerokoscLinii++)
                    {
                        UstawKolor(temp, wymiarOkna.Height - 20 - 75 + dlugoscLinii, wymiarOkna.Width / 4 + szerokoscLinii, 255);
                    }
                }

                //pole bramkowe gora
                for (int dlugoscLinii = 0; dlugoscLinii < 25; dlugoscLinii++)
                {
                    for (int szerokoscLinii = 0; szerokoscLinii < 10; szerokoscLinii++)
                    {
                        UstawKolor(temp, 20 + dlugoscLinii, 180 + szerokoscLinii, 255);
                    }
                }
                for (int dlugoscLinii = 0; dlugoscLinii < wymiarOkna.Width - 2*180 +1; dlugoscLinii++)
                {
                    for (int szerokoscLinii = 0; szerokoscLinii < 10; szerokoscLinii++)
                    {
                        UstawKolor(temp, 20 + 25 + szerokoscLinii, 180 + dlugoscLinii, 255);
                    }
                }
                for (int dlugoscLinii = 0; dlugoscLinii < 25; dlugoscLinii++)
                {
                    for (int szerokoscLinii = 0; szerokoscLinii < 10; szerokoscLinii++)
                    {
                        UstawKolor(temp, 20 + 25 - dlugoscLinii, wymiarOkna.Width - 180 - szerokoscLinii, 255);
                    }
                }

                //pole bramkowe dol
                for (int dlugoscLinii = 0; dlugoscLinii < 25; dlugoscLinii++)
                {
                    for (int szerokoscLinii = 0; szerokoscLinii < 10; szerokoscLinii++)
                    {
                        UstawKolor(temp, wymiarOkna.Height - 20 - dlugoscLinii, wymiarOkna.Width - 180 - szerokoscLinii, 255);
                    }
                }
                for (int dlugoscLinii = 0; dlugoscLinii < wymiarOkna.Width - 2 * 180 + 1; dlugoscLinii++)
                {
                    for (int szerokoscLinii = 0; szerokoscLinii < 10; szerokoscLinii++)
                    {
                        UstawKolor(temp, wymiarOkna.Height - 20 - 25 - szerokoscLinii, wymiarOkna.Width - 180 - dlugoscLinii, 255);
                    }
                }
                for (int dlugoscLinii = 0; dlugoscLinii < 25; dlugoscLinii++)
                {
                    for (int szerokoscLinii = 0; szerokoscLinii < 10; szerokoscLinii++)
                    {
                        UstawKolor(temp, wymiarOkna.Height - 20 - 25 + dlugoscLinii, 180 + szerokoscLinii, 255);
                    }
                }

                //okrag srodek
                for (int x = 0; x < wymiarOkna.Width; x++)
                {
                    for (int szerokoscLinii = 0; szerokoscLinii < 10; szerokoscLinii++)
                    {
                        int y = (int)(Math.Sqrt(Math.Pow(77 + szerokoscLinii, 2) - Math.Pow((x - 233), 2)) + 328);

                        if (y > 0)
                        {
                            UstawKolor(temp, y, x, 255);

                            UstawKolor(temp, wymiarOkna.Height - y, x, 255);
                        }

                    }
                }

                //polokregi
                for (int x = 0; x < wymiarOkna.Width; x++)
                {
                    for (int szerokoscLinii = 0; szerokoscLinii < 10; szerokoscLinii++)
                    {
                        int y = (int)(Math.Sqrt(Math.Pow(50 + szerokoscLinii, 2) - Math.Pow((x - 233), 2)) + 75);

                        if (y > 100 && y < 590)
                        {
                            UstawKolor(temp, y, x, 255);

                            UstawKolor(temp, wymiarOkna.Height - y, x, 255);
                        }

                    }
                }

                //lewy gorny maly luk
                for (int i = 0; i < 10; i++) RysujLuk(temp, 20, 20, 30 + i, 0, 90);

                //prawy gorny maly luk
                for (int i = 0; i < 10; i++) RysujLuk(temp, wymiarOkna.Width - 20, 20, 30 + i, -90, 0);

                //lewy dolny maly luk
                for (int i = 0; i < 10; i++) RysujLuk(temp, 20, wymiarOkna.Height - 20, 30 + i, 90, 180);

                //prawy dolny maly luk
                for (int i = 0; i < 10; i++) RysujLuk(temp, wymiarOkna.Width - 20, wymiarOkna.Height - 20, 30 + i, -180, -90);
            }
            if (comboBoxTypBoiska.SelectedIndex == 1)//koszykowka
            {
                imageBoisko.SetValue(new MCvScalar(230, 70, 0)); //boisko do koszykowki
                comboBoxRysowanie.Enabled = true;

                //linia lewa
                for (int dlugoscLinii = 0; dlugoscLinii < wymiarOkna.Height - 40; dlugoscLinii++)
                {
                    for (int szerokoscLinii = 0; szerokoscLinii < 10; szerokoscLinii++)
                    {
                        UstawKolor(temp, wymiarOkna.Height - 20 - dlugoscLinii, 20 + szerokoscLinii, 255);
                    }
                }
                //linia gorna
                for (int dlugoscLinii = 0; dlugoscLinii < wymiarOkna.Width - 40; dlugoscLinii++)
                {
                    for (int szerokoscLinii = 0; szerokoscLinii < 10; szerokoscLinii++)
                    {
                        UstawKolor(temp, 20 + szerokoscLinii, 20 + dlugoscLinii, 255);
                    }
                }
                //linia prawa
                for (int dlugoscLinii = 0; dlugoscLinii < wymiarOkna.Height - 40; dlugoscLinii++)
                {
                    for (int szerokoscLinii = 0; szerokoscLinii < 10; szerokoscLinii++)
                    {
                        UstawKolor(temp, 20 + dlugoscLinii, wymiarOkna.Width - 20 - szerokoscLinii, 255);
                    }
                }
                //linia dolna
                for (int dlugoscLinii = 0; dlugoscLinii < wymiarOkna.Width - 40; dlugoscLinii++)
                {
                    for (int szerokoscLinii = 0; szerokoscLinii < 10; szerokoscLinii++)
                    {
                        UstawKolor(temp, wymiarOkna.Height - 20 - szerokoscLinii, wymiarOkna.Width - 20 - dlugoscLinii, 255);
                    }
                }
                //linia srodka
                for (int dlugoscLinii = 0; dlugoscLinii < wymiarOkna.Width - 40; dlugoscLinii++)
                {
                    for (int szerokoscLinii = 0; szerokoscLinii < 10; szerokoscLinii++)
                    {
                        UstawKolor(temp, wymiarOkna.Height / 2 + 5 - szerokoscLinii, 20 + dlugoscLinii, 255);
                    }
                }

                //linia krotka lewa gora
                for (int dlugoscLinii = 0; dlugoscLinii < wymiarOkna.Height /8; dlugoscLinii++)
                {
                    for (int szerokoscLinii = -5; szerokoscLinii < 5; szerokoscLinii++)
                    {
                        UstawKolor(temp, 20 + dlugoscLinii, 50 + szerokoscLinii, 255);
                    }
                }
                //luk duzy gora
                for (int szerokoscLinii = -5; szerokoscLinii < 5; szerokoscLinii++)
                {
                    RysujLuk(temp, wymiarOkna.Width / 2, 25, (wymiarOkna.Width - 75) / 2 + szerokoscLinii, -70, 70);
                }
                //linia krotka prawa gora
                for (int dlugoscLinii = 0; dlugoscLinii < wymiarOkna.Height /8; dlugoscLinii++)
                {
                    for (int szerokoscLinii = -5; szerokoscLinii < 5; szerokoscLinii++)
                    {
                        UstawKolor(temp, wymiarOkna.Height/8 +20 - dlugoscLinii, wymiarOkna.Width - 50 + szerokoscLinii, 255);
                    }
                }

                //linia srednia lewa gora x150,y130
                for (int dlugoscLinii = 0; dlugoscLinii < 130; dlugoscLinii++)
                {
                    for (int szerokoscLinii = -5; szerokoscLinii < 5; szerokoscLinii++)
                    {
                        UstawKolor(temp, 20 + dlugoscLinii, 150 + szerokoscLinii, 255);
                    }
                }
                //luk sredni gora
                for (int szerokoscLinii = -5; szerokoscLinii < 5; szerokoscLinii++)
                {
                    RysujLuk(temp, wymiarOkna.Width / 2, 130, 55 + szerokoscLinii, -70, 70);
                }
                //linia srednia prawa gora
                for (int dlugoscLinii = 0; dlugoscLinii < 130; dlugoscLinii++)
                {
                    for (int szerokoscLinii = -5; szerokoscLinii < 5; szerokoscLinii++)
                    {
                        UstawKolor(temp, 130+20 - dlugoscLinii, wymiarOkna.Width - 150 + szerokoscLinii, 255);
                    }
                }
                //linia srednia gora
                for (int dlugoscLinii = 0; dlugoscLinii < wymiarOkna.Width-300; dlugoscLinii++)
                {
                    for (int szerokoscLinii = -5; szerokoscLinii < 5; szerokoscLinii++)
                    {
                        UstawKolor(temp, 150-5+ szerokoscLinii, 150 + dlugoscLinii, 255);
                    }
                }

                //luk maly gora
                for (int szerokoscLinii = -5; szerokoscLinii < 5; szerokoscLinii++)
                {
                    RysujLuk(temp, wymiarOkna.Width / 2, 60, 40 + szerokoscLinii, -100, 100);
                }

                //okrag srodek
                for (int x = 0; x < wymiarOkna.Width; x++)
                {
                    for (int szerokoscLinii = 0; szerokoscLinii < 10; szerokoscLinii++)
                    {
                        int y = (int)(Math.Sqrt(Math.Pow(50 + szerokoscLinii, 2) - Math.Pow((x - 233), 2)) + 328);

                        if (y > 0)
                        {
                            UstawKolor(temp, y, x, 255);
                            UstawKolor(temp, wymiarOkna.Height - y, x, 255);
                        }

                    }
                }

                //linia krotka lewa dol
                for (int dlugoscLinii = 0; dlugoscLinii < wymiarOkna.Height / 8; dlugoscLinii++)
                {
                    for (int szerokoscLinii = -5; szerokoscLinii < 5; szerokoscLinii++)
                    {
                        UstawKolor(temp, wymiarOkna.Height - 20 - dlugoscLinii, 50 + szerokoscLinii, 255);
                    }
                }
                //luk duzy dol
                for (int szerokoscLinii = -5; szerokoscLinii < 5; szerokoscLinii++)
                {
                    RysujLuk(temp, wymiarOkna.Width / 2, wymiarOkna.Height - 25, (wymiarOkna.Width - 75) / 2 + szerokoscLinii, 180-70, 180+70);
                }
                //linia krotka prawa dol
                for (int dlugoscLinii = 0; dlugoscLinii < wymiarOkna.Height / 8; dlugoscLinii++)
                {
                    for (int szerokoscLinii = -5; szerokoscLinii < 5; szerokoscLinii++)
                    {
                        UstawKolor(temp, wymiarOkna.Height - (wymiarOkna.Height / 8 + 20 - dlugoscLinii), wymiarOkna.Width - 50 + szerokoscLinii, 255);
                    }
                }

                //linia srednia lewa dol x150,y130
                for (int dlugoscLinii = 0; dlugoscLinii < 130; dlugoscLinii++)
                {
                    for (int szerokoscLinii = -5; szerokoscLinii < 5; szerokoscLinii++)
                    {
                        UstawKolor(temp, wymiarOkna.Height - (20 + dlugoscLinii), 150 + szerokoscLinii, 255);
                    }
                }
                //luk sredni dol
                for (int szerokoscLinii = -5; szerokoscLinii < 5; szerokoscLinii++)
                {
                    RysujLuk(temp, wymiarOkna.Width / 2, wymiarOkna.Height - 130, 55 + szerokoscLinii, 180-70, 180+70);
                }
                //linia srednia prawa dol
                for (int dlugoscLinii = 0; dlugoscLinii < 130; dlugoscLinii++)
                {
                    for (int szerokoscLinii = -5; szerokoscLinii < 5; szerokoscLinii++)
                    {
                        UstawKolor(temp, wymiarOkna.Height - (130 + 20 - dlugoscLinii), wymiarOkna.Width - 150 + szerokoscLinii, 255);
                    }
                }
                //linia srednia dol
                for (int dlugoscLinii = 0; dlugoscLinii < wymiarOkna.Width - 300; dlugoscLinii++)
                {
                    for (int szerokoscLinii = -5; szerokoscLinii < 5; szerokoscLinii++)
                    {
                        UstawKolor(temp, wymiarOkna.Height - (150 - 5) + szerokoscLinii, 150 + dlugoscLinii, 255);
                    }
                }

                //luk maly dol
                for (int szerokoscLinii = -5; szerokoscLinii < 5; szerokoscLinii++)
                {
                    RysujLuk(temp, wymiarOkna.Width / 2, wymiarOkna.Height - 60, 40 + szerokoscLinii, 180-100, 180+100);
                }
            }

            imageBoisko.Data = temp;
            pictureBoxBoisko.Image = imageBoisko.Bitmap;
        }

        private void comboBoxRysowanie_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxRysowanie.SelectedIndex == 0)//powielanie linii
            {
                buttonStart.Enabled = true;
            }
            if (comboBoxRysowanie.SelectedIndex == 1)//nowe linie
            {
                buttonStart.Enabled = true;
            }
        }

        private void pictureBoxBoisko_MouseClick(object sender, MouseEventArgs e)
        {
            labelPozycja.Text = "X=" + e.X + ",Y= " + e.Y;
        }

        private void RysujLuk(byte[,,] temp, int pozX, int pozY, int promien, int odKat, int doKat)
        {
            for (int kat = odKat; kat <= doKat; kat++)
            {
                int x = (int)(Math.Sin(stopnieNaRad((double)kat)) * promien + pozX);
                int y = (int)(Math.Cos(stopnieNaRad((double)kat)) * promien + pozY);

                //Console.WriteLine(Math.Sin(stopnieNaRad((double)kat)));
                //Console.WriteLine("x=" + x + ", y=" + y);

                temp[y, x, 0] = 255;
                temp[y, x, 1] = 255;
                temp[y, x, 2] = 255;
            }
        }

        private double stopnieNaRad(double stopnie)
        {
            //Console.WriteLine("stopnie=" + stopnie);
            return (stopnie * Math.PI) / 180;
        }

        private void UstawKolor(byte[,,] temp, int y, int x, int kolor)
        {
            temp[y, x, 0] = (byte)kolor;
            temp[y, x, 1] = (byte)kolor;
            temp[y, x, 2] = (byte)kolor;

            //listBox1.Items.Add("X=" + x + ",Y=" + y);
        }

        private void UstawRobota(byte[,,] temp, int y, int x, int R, int G, int B)
        {
            string Text = "Pozycja robota: X=" + x + ",Y=" + y;
            
            if (etapyPilkaNozna[ktoryEtap].czyMalowane == 'm') Text+=", maluje";
            else Text+=", nie maluje";

            if (etapyPilkaNozna[ktoryEtap].liniaCzyLuk == "linia") Text += ", VL=5, VR=5";
            else if(etapyPilkaNozna[ktoryEtap].odKat> etapyPilkaNozna[ktoryEtap].doKat) Text += ", VL=10, VR=5";
            else Text += ", VL=5, VR=10";

            listBox1.Items.Add(Text);
            labelPozycjaRobota.Text = Text;

            for (int i = -10; i < 10; i++)
            {
                for (int j = -10; j < 10; j++)
                {
                    temp[y + j, x + i, 0] = (byte)R;
                    temp[y + j, x + i, 1] = (byte)G;
                    temp[y + j, x + i, 2] = (byte)B;
                }
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
            buttonStop.Visible = true;
            buttonAnuluj.Visible = false;

            comboBoxTrybPracy.Enabled = false;
            comboBoxTypBoiska.Enabled = false;
            comboBoxRysowanie.Enabled = false;

            if (krok == 0)
            {
                byte[,,] temp = imageBoisko.Data;

                for (int x = 0; x < wymiarOkna.Width; x++)
                {
                    for (int y = 0; y < wymiarOkna.Height; y++)
                    {
                        if (temp[y, x, 0] == 255) UstawKolor(temp, y, x, 180);
                    }
                }

                imageBoisko.Data = temp;
                pictureBoxBoisko.Image = imageBoisko.Bitmap;
            }

            timer1.Interval = (int)numericUpDownKrok.Value;
            timer1.Enabled = true;
        }

        private void buttonLogi_Click(object sender, EventArgs e)
        {
            if (listBox1.Visible == false) listBox1.Visible = true;
            else listBox1.Visible = false;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            buttonStop.Enabled = false;
            buttonStart.Enabled = true;
            buttonAnuluj.Visible = true;

            timer1.Enabled = false;

        }

        private void buttonAnuluj_Click(object sender, EventArgs e)
        {
            buttonAnuluj.Visible = false;
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;

            comboBoxTrybPracy.Enabled = true;
            comboBoxTypBoiska.Enabled = true;
            comboBoxRysowanie.Enabled = true;

            listBox1.Items.Clear();

            int pomoc = comboBoxTypBoiska.SelectedIndex;
            comboBoxTypBoiska.SelectedIndex = -1;
            comboBoxTypBoiska.SelectedIndex = pomoc;

            krok = 0;
            krokPomoc = 0;
            ktoryEtap = 0;
            labelKrok.Text = "" + krok;

            progressBar1.Value = 0;
        }

        
        private void timer1_Tick(object sender, EventArgs e)
        {
            labelKrok.Text = "" + krok;
            //listBox1.TopIndex = listBox1.ItemHeight;
            

            byte[,,] tempBoisko = imageBoisko.Data;
            byte[,,] tempBoiskoZRobotem;

            if(etapyPilkaNozna[ktoryEtap].liniaCzyLuk=="linia") //linia
            {
                //if (krok >= krokPomoc && krok < krokPomoc + etapyPilkaNozna[ktoryEtap].ileKrokow)
                //{

                if (etapyPilkaNozna[ktoryEtap].czyMalowane == 'm')
                {
                    for (int szerokoscLinii = -(etapyPilkaNozna[ktoryEtap].szerokoscLinii / 2); szerokoscLinii < etapyPilkaNozna[ktoryEtap].szerokoscLinii / 2; szerokoscLinii++)
                    {
                        if (etapyPilkaNozna[ktoryEtap].Xpocz == etapyPilkaNozna[ktoryEtap].Xkon)//linia pionowa
                        {
                            if (etapyPilkaNozna[ktoryEtap].Ykon < etapyPilkaNozna[ktoryEtap].Ypocz)//linia w gore
                                UstawKolor(tempBoisko, etapyPilkaNozna[ktoryEtap].Ypocz - (krok - krokPomoc), etapyPilkaNozna[ktoryEtap].Xpocz + szerokoscLinii, 255); //rysuj boisko

                            else if (etapyPilkaNozna[ktoryEtap].Ykon > etapyPilkaNozna[ktoryEtap].Ypocz) //linia w dol
                                UstawKolor(tempBoisko, etapyPilkaNozna[ktoryEtap].Ypocz + (krok - krokPomoc), etapyPilkaNozna[ktoryEtap].Xpocz + szerokoscLinii, 255);
                        }

                        else if (etapyPilkaNozna[ktoryEtap].Ypocz == etapyPilkaNozna[ktoryEtap].Ykon)//linia pozioma
                        {
                            if (etapyPilkaNozna[ktoryEtap].Xkon < etapyPilkaNozna[ktoryEtap].Xpocz)//linia w lewo    
                                UstawKolor(tempBoisko, etapyPilkaNozna[ktoryEtap].Ypocz + szerokoscLinii, etapyPilkaNozna[ktoryEtap].Xpocz - (krok - krokPomoc), 255);

                            else if (etapyPilkaNozna[ktoryEtap].Xkon > etapyPilkaNozna[ktoryEtap].Xpocz) //linia w prawo
                                UstawKolor(tempBoisko, etapyPilkaNozna[ktoryEtap].Ypocz + szerokoscLinii, etapyPilkaNozna[ktoryEtap].Xpocz + (krok - krokPomoc), 255);
                        }
                        
                        else
                        {
                            if (etapyPilkaNozna[ktoryEtap].Ykon < etapyPilkaNozna[ktoryEtap].Ypocz && etapyPilkaNozna[ktoryEtap].Xkon < etapyPilkaNozna[ktoryEtap].Xpocz)//linia w gore w lewo 
                            {
                                UstawKolor(tempBoisko, etapyPilkaNozna[ktoryEtap].Ypocz - (int)(((double)(etapyPilkaNozna[ktoryEtap].Ypocz - etapyPilkaNozna[ktoryEtap].Ykon) / etapyPilkaNozna[ktoryEtap].ileKrokow) * (krok - krokPomoc)) + szerokoscLinii, etapyPilkaNozna[ktoryEtap].Xpocz - (int)(((double)(etapyPilkaNozna[ktoryEtap].Xpocz - etapyPilkaNozna[ktoryEtap].Xkon) / etapyPilkaNozna[ktoryEtap].ileKrokow) * (krok - krokPomoc)) + szerokoscLinii, 255);
                            }
                            else if (etapyPilkaNozna[ktoryEtap].Ykon < etapyPilkaNozna[ktoryEtap].Ypocz && etapyPilkaNozna[ktoryEtap].Xkon > etapyPilkaNozna[ktoryEtap].Xpocz)//linia w gore w prawo
                            {
                                UstawKolor(tempBoisko, etapyPilkaNozna[ktoryEtap].Ypocz - (int)(((double)(etapyPilkaNozna[ktoryEtap].Ypocz - etapyPilkaNozna[ktoryEtap].Ykon) / etapyPilkaNozna[ktoryEtap].ileKrokow) * (krok - krokPomoc)) + szerokoscLinii, etapyPilkaNozna[ktoryEtap].Xpocz + (int)(((double)(etapyPilkaNozna[ktoryEtap].Xkon - etapyPilkaNozna[ktoryEtap].Xpocz) / etapyPilkaNozna[ktoryEtap].ileKrokow) * (krok - krokPomoc)) + szerokoscLinii, 255);
                            }
                            else if (etapyPilkaNozna[ktoryEtap].Ykon > etapyPilkaNozna[ktoryEtap].Ypocz && etapyPilkaNozna[ktoryEtap].Xkon < etapyPilkaNozna[ktoryEtap].Xpocz)//linia w dol w lewo 
                            {
                                UstawKolor(tempBoisko, etapyPilkaNozna[ktoryEtap].Ypocz + (int)(((double)(etapyPilkaNozna[ktoryEtap].Ykon - etapyPilkaNozna[ktoryEtap].Ypocz) / etapyPilkaNozna[ktoryEtap].ileKrokow) * (krok - krokPomoc)) + szerokoscLinii, etapyPilkaNozna[ktoryEtap].Xpocz - (int)(((double)(etapyPilkaNozna[ktoryEtap].Xpocz - etapyPilkaNozna[ktoryEtap].Xkon) / etapyPilkaNozna[ktoryEtap].ileKrokow) * (krok - krokPomoc)) + szerokoscLinii, 255);
                            }
                            else if (etapyPilkaNozna[ktoryEtap].Ykon > etapyPilkaNozna[ktoryEtap].Ypocz && etapyPilkaNozna[ktoryEtap].Xkon > etapyPilkaNozna[ktoryEtap].Xpocz)//linia w dol w prawo 
                            {
                                UstawKolor(tempBoisko, etapyPilkaNozna[ktoryEtap].Ypocz + (int)(((double)(etapyPilkaNozna[ktoryEtap].Ykon - etapyPilkaNozna[ktoryEtap].Ypocz) / etapyPilkaNozna[ktoryEtap].ileKrokow) * (krok - krokPomoc)) + szerokoscLinii, etapyPilkaNozna[ktoryEtap].Xpocz + (int)(((double)(etapyPilkaNozna[ktoryEtap].Xkon - etapyPilkaNozna[ktoryEtap].Xpocz) / etapyPilkaNozna[ktoryEtap].ileKrokow) * (krok - krokPomoc)) + szerokoscLinii, 255);
                            }
                        }
                        /*
                        else
                        {
                            Console.WriteLine("Nie wiem jaka to linia (pozioma czy pionowa)! - spelnij zaleznosc: Xp==Xk LUB Yp==Yk");
                            Console.WriteLine("Xp=" + etapyPilkaNozna[ktoryEtap].Xpocz + " Yp=" + etapyPilkaNozna[ktoryEtap].Ypocz);
                            Console.WriteLine("Xk=" + etapyPilkaNozna[ktoryEtap].Xkon + " Yk=" + etapyPilkaNozna[ktoryEtap].Ykon);
                        }*/
                    }
                }
                
                imageBoisko.Data = tempBoisko; //przypisz ponownie

                imageBoiskoZRobotem.Data = imageBoisko.Data; //zrob kopie boiska (bez robota)

                tempBoiskoZRobotem = imageBoiskoZRobotem.Data; //zacznij edycje boiska z robotem

                if (etapyPilkaNozna[ktoryEtap].Xpocz == etapyPilkaNozna[ktoryEtap].Xkon)//linia pionowa
                { 
                    if (etapyPilkaNozna[ktoryEtap].Ykon < etapyPilkaNozna[ktoryEtap].Ypocz)//linia w gore
                        UstawRobota(tempBoiskoZRobotem, etapyPilkaNozna[ktoryEtap].Ypocz - (krok - krokPomoc), etapyPilkaNozna[ktoryEtap].Xpocz, 255, 0, 0); //ustaw robota

                    else if (etapyPilkaNozna[ktoryEtap].Ykon > etapyPilkaNozna[ktoryEtap].Ypocz) //linia w dol
                        UstawRobota(tempBoiskoZRobotem, etapyPilkaNozna[ktoryEtap].Ypocz + (krok - krokPomoc), etapyPilkaNozna[ktoryEtap].Xpocz, 255, 0, 0);
                }

                else if (etapyPilkaNozna[ktoryEtap].Ypocz == etapyPilkaNozna[ktoryEtap].Ykon)//linia pozioma
                {
                    if (etapyPilkaNozna[ktoryEtap].Xkon < etapyPilkaNozna[ktoryEtap].Xpocz)//linia w lewo
                        UstawRobota(tempBoiskoZRobotem, etapyPilkaNozna[ktoryEtap].Ypocz, etapyPilkaNozna[ktoryEtap].Xpocz - (krok - krokPomoc), 255, 0, 0);

                    else if (etapyPilkaNozna[ktoryEtap].Xkon > etapyPilkaNozna[ktoryEtap].Xpocz) //linia w prawo
                        UstawRobota(tempBoiskoZRobotem, etapyPilkaNozna[ktoryEtap].Ypocz, etapyPilkaNozna[ktoryEtap].Xpocz + (krok - krokPomoc), 255, 0, 0); //ustaw robota
                }

                else if (etapyPilkaNozna[ktoryEtap].Ykon < etapyPilkaNozna[ktoryEtap].Ypocz && etapyPilkaNozna[ktoryEtap].Xkon < etapyPilkaNozna[ktoryEtap].Xpocz)//linia w gore w lewo 
                {
                    UstawRobota(tempBoiskoZRobotem, etapyPilkaNozna[ktoryEtap].Ypocz - (int)(((double)(etapyPilkaNozna[ktoryEtap].Ypocz - etapyPilkaNozna[ktoryEtap].Ykon) / etapyPilkaNozna[ktoryEtap].ileKrokow) * (krok - krokPomoc)), etapyPilkaNozna[ktoryEtap].Xpocz - (int)(((double)(etapyPilkaNozna[ktoryEtap].Xpocz - etapyPilkaNozna[ktoryEtap].Xkon) / etapyPilkaNozna[ktoryEtap].ileKrokow) * (krok - krokPomoc)), 255, 0, 0);
                }
                else if (etapyPilkaNozna[ktoryEtap].Ykon < etapyPilkaNozna[ktoryEtap].Ypocz && etapyPilkaNozna[ktoryEtap].Xkon > etapyPilkaNozna[ktoryEtap].Xpocz)//linia w gore w prawo
                {
                    UstawRobota(tempBoiskoZRobotem, etapyPilkaNozna[ktoryEtap].Ypocz - (int)(((double)(etapyPilkaNozna[ktoryEtap].Ypocz - etapyPilkaNozna[ktoryEtap].Ykon) / etapyPilkaNozna[ktoryEtap].ileKrokow) * (krok - krokPomoc)), etapyPilkaNozna[ktoryEtap].Xpocz + (int)(((double)(etapyPilkaNozna[ktoryEtap].Xkon - etapyPilkaNozna[ktoryEtap].Xpocz) / etapyPilkaNozna[ktoryEtap].ileKrokow) * (krok - krokPomoc)), 255, 0, 0);
                }
                else if (etapyPilkaNozna[ktoryEtap].Ykon > etapyPilkaNozna[ktoryEtap].Ypocz && etapyPilkaNozna[ktoryEtap].Xkon < etapyPilkaNozna[ktoryEtap].Xpocz)//linia w dol w lewo 
                {
                    UstawRobota(tempBoiskoZRobotem, etapyPilkaNozna[ktoryEtap].Ypocz + (int)(((double)(etapyPilkaNozna[ktoryEtap].Ykon - etapyPilkaNozna[ktoryEtap].Ypocz) / etapyPilkaNozna[ktoryEtap].ileKrokow) * (krok - krokPomoc)), etapyPilkaNozna[ktoryEtap].Xpocz - (int)(((double)(etapyPilkaNozna[ktoryEtap].Xpocz - etapyPilkaNozna[ktoryEtap].Xkon) / etapyPilkaNozna[ktoryEtap].ileKrokow) * (krok - krokPomoc)), 255, 0, 0);
                }
                else if (etapyPilkaNozna[ktoryEtap].Ykon > etapyPilkaNozna[ktoryEtap].Ypocz && etapyPilkaNozna[ktoryEtap].Xkon > etapyPilkaNozna[ktoryEtap].Xpocz)//linia w dol w prawo 
                {
                    UstawRobota(tempBoiskoZRobotem, etapyPilkaNozna[ktoryEtap].Ypocz + (int)(((double)(etapyPilkaNozna[ktoryEtap].Ykon - etapyPilkaNozna[ktoryEtap].Ypocz) / etapyPilkaNozna[ktoryEtap].ileKrokow) * (krok - krokPomoc)), etapyPilkaNozna[ktoryEtap].Xpocz + (int)(((double)(etapyPilkaNozna[ktoryEtap].Xkon - etapyPilkaNozna[ktoryEtap].Xpocz) / etapyPilkaNozna[ktoryEtap].ileKrokow) * (krok - krokPomoc)), 255, 0, 0);
                }

                imageBoiskoZRobotem.Data = tempBoiskoZRobotem; //przypisz ponownie

                //}
            }
            else if(etapyPilkaNozna[ktoryEtap].liniaCzyLuk == "luk") //luk
            {
                int x=0;
                int y=0;
                
                for (int szerokosc = -(etapyPilkaNozna[ktoryEtap].szerokoscLinii/2); szerokosc < etapyPilkaNozna[ktoryEtap].szerokoscLinii/2; szerokosc++)
                {
                    x = (int)(Math.Sin(stopnieNaRad(etapyPilkaNozna[ktoryEtap].odKat + (krok - krokPomoc) * ((double)(etapyPilkaNozna[ktoryEtap].doKat - etapyPilkaNozna[ktoryEtap].odKat) / etapyPilkaNozna[ktoryEtap].ileKrokow))) * (etapyPilkaNozna[ktoryEtap].promien + szerokosc) + etapyPilkaNozna[ktoryEtap].Xcen);
                    y = (int)(Math.Cos(stopnieNaRad(etapyPilkaNozna[ktoryEtap].odKat + (krok - krokPomoc) * ((double)(etapyPilkaNozna[ktoryEtap].doKat - etapyPilkaNozna[ktoryEtap].odKat) / etapyPilkaNozna[ktoryEtap].ileKrokow))) * (etapyPilkaNozna[ktoryEtap].promien + szerokosc) + etapyPilkaNozna[ktoryEtap].Ycen);

                    tempBoisko[y, x, 0] = 255;
                    tempBoisko[y, x, 1] = 255;
                    tempBoisko[y, x, 2] = 255;
                }

                imageBoisko.Data = tempBoisko; //przypisz ponownie

                imageBoiskoZRobotem.Data = imageBoisko.Data; //zrob kopie boiska (bez robota)

                tempBoiskoZRobotem = imageBoiskoZRobotem.Data; //zacznij edycje boiska z robotem

                UstawRobota(tempBoiskoZRobotem, y, x, 255, 0, 0);
            }

            if (krok == krokPomoc + etapyPilkaNozna[ktoryEtap].ileKrokow)
            {
                krokPomoc += etapyPilkaNozna[ktoryEtap].ileKrokow;
                ktoryEtap++;
            }

            pictureBoxBoisko.Image = imageBoiskoZRobotem.Bitmap; //narysuj

            krok++;

            if (ktoryEtap == etapyPilkaNozna.Length) timer1.Enabled = false;
        }

        private void numericUpDownKrok_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = (int)numericUpDownKrok.Value;
        }

        private void labelKrok_TextChanged(object sender, EventArgs e)
        {
            if (krok % 150 == 0)
            {
                bateria--;
                labelBateria.Text = bateria + "%";
                if (bateria == 0) timer1.Enabled = false;
                if (bateria > 50) labelBateria.ForeColor = Color.Green;
                else if (bateria > 30) labelBateria.ForeColor = Color.Yellow;
                else labelBateria.ForeColor = Color.Red;
            }
            
            // pilka nozna 6695 krokow
            progressBar1.PerformStep();

            double wynik = (krok *100) / 6695;
            labelProcent.Text = wynik + "%";
        }

        private void buttonSet1_Click(object sender, EventArgs e)
        {
            numericUpDownKrok.Value = 1;
        }

        
    }
}
