
using DataManager;
using Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace UI
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private const string serverAddress = "http://localhost:54760";
        private const string GET_CELEBS_COMMAND = "GetActors";
        private const string DELETE_CELEB_COMMAND = "RemoveActorAsync";
        private const string RESTORE_CELEB_COMMAND = "ResetAsync";
        public Logic logic;
        public ConcurrentDictionary<string, Actor> actors;

        public Form1()
        {
            InitializeComponent();
       
        }

        private void Button2_Click(object sender, EventArgs e)
        {



        }

        private async void Dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv1.Columns[e.ColumnIndex].Name == "Remove")
            {
                var currentActor = actorBindingSource.Current;


                var celebrities = JsonConvert.DeserializeObject<List<Actor>>(await (await client.PostAsync($"{serverAddress}/values/{DELETE_CELEB_COMMAND}", new StringContent(JsonConvert.SerializeObject(currentActor), Encoding.UTF8, "application/json"))).Content.ReadAsStringAsync());


                actorBindingSource.RemoveCurrent();


            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            update();

        }

        public async void update()
        {
            var celebrities = JsonConvert.DeserializeObject<List<Actor>>(await client.GetStringAsync($"{serverAddress}/values/{GET_CELEBS_COMMAND}"));
               

            foreach (Actor actor in celebrities)
            {
                actorBindingSource.Add(actor);
            }
        }



        private void Button1_Click(object sender, EventArgs e)
        {

            reset();
            removeAll();
            update();
        }

        private async void reset() {
         
            var celebrities = JsonConvert.DeserializeObject<List<Actor>>(await (await client.PostAsync($"{serverAddress}/values/{RESTORE_CELEB_COMMAND}", new StringContent("", Encoding.UTF8, "application/json"))).Content.ReadAsStringAsync());
         
            foreach (Actor actor in celebrities)
            {
                actorBindingSource.Add(actor);
            }
        }

        public void removeAll()
        {
            actorBindingSource.Clear();
        }
    }
}

