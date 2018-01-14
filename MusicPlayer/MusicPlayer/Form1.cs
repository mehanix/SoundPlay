using AxWMPLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace MusicPlayer
{
    public partial class Form1 : Form
    {
        public WMPLib.WindowsMediaPlayer wplayer;
        public IWMPPlaylist pl;
        public struct song
        {
            public string path { get; set; }
            public string name { get; set; }
        }
        BindingList<song> playlist;


        public Form1()
        {
            InitializeComponent();
            playlist = new BindingList<song>();
            listBox1.DataSource = playlist;
            listBox1.DisplayMember = "name";
            listBox1.ValueMember = "path";
            
            wplayer = new WindowsMediaPlayer();
            
            wplayer.CurrentItemChange += Wplayer_CurrentItemChange;
            wplayer.settings.volume = 50;
        }

        private void Wplayer_CurrentItemChange(object pdispMedia)
        {
           // MessageBox.Show("AAAAAAAAAAAA ");

            int index=0;
            for (int i = 0; i < playlist.Count - 1; i++)
            {
                if (wplayer.currentMedia.isIdentical[pl.Item[i]])
                {
                    index = i;
                    
                    break;
                }
            }

            listBox1.SelectedIndex = index;
            label2.Text = "Now playing: " + wplayer.currentMedia.name;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
     
            opf.ShowDialog();
            
            MessageBox.Show(opf.FileName);
            song selection = new song();
            selection.path = opf.FileName;
            selection.name = opf.SafeFileName;
            
            playlist.Add(selection);
            //wplayer.URL = opf.FileName;
            //wplayer.controls.play();

        }

        private void wplayer_PositionChange(object sender, _WMPOCXEvents_PositionChangeEvent e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(listBox1.SelectedValue.ToString());
            wplayer.URL = listBox1.SelectedValue.ToString();
            wplayer.controls.play();
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            wplayer.controls.stop();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //scos iteme din playlistul vechi

            pl = wplayer.newPlaylist("pl","");
            wplayer.currentPlaylist = pl;
           
            foreach (song s in playlist)
            {
                //creat nou obiect "media"
                IWMPMedia sng = wplayer.newMedia(s.path);
                //adaugat la playlistul curent :D
                wplayer.currentPlaylist.appendItem(sng);
            }
            wplayer.controls.play();
            //listBox1.SelectedIndex = 0;
            
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            wplayer.controls.next();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            playlist.RemoveAt(listBox1.SelectedIndex);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            //MessageBox.Show(trackBar1.Value.ToString());
            wplayer.settings.volume = trackBar1.Value;
        }
    }
}
