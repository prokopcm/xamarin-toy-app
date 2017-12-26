using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Media;

namespace XamarinTestApp.Droid.Fragments
{
    public class AudioFragment : Fragment
    {

        MediaPlayer _player;
        Button btnPlayAudio;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.AudioFragment, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            _player = MediaPlayer.Create(this.Context, Resource.Raw.pug_pinball);
            btnPlayAudio = (Button) this.View.FindViewById(Resource.Id.btnPlayAudio);

            btnPlayAudio.Click += BtnPlayAudio_Click;
        }

        private void BtnPlayAudio_Click(object sender, EventArgs e)
        {
            _player.Start();
        }
    }
}