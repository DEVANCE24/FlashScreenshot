using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.IO;
using Xamarin.Essentials;

namespace flashscreenshot
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button turnon, turnoff, take;
        private ImageView image;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.activity_main);
            UIReference();
            UIClickEvents();
        }

        private void UIClickEvents()
        {
            turnoff.Click += Turnoff_Click;
            turnon.Click += Turnon_Click;
            take.Click += Take_Click;

        }

        private async void Take_Click(object sender, EventArgs e)
        {
            if (Screenshot.IsCaptureSupported)
            {
                ScreenshotResult screenshot = await Screenshot.CaptureAsync();
                Stream stream = await screenshot.OpenReadAsync();
                image.SetImageBitmap(BitmapFactory.DecodeStream(stream));

            }
        }

        private async void Turnon_Click(object sender, EventArgs e)
        {
            try
            {
                // Turn On
                await Flashlight.TurnOnAsync();


            }
            catch (FeatureNotSupportedException fnsEx)
            {
                Log.Debug("Flashhhhhhhhhhhhhhhhhh", fnsEx.Message);
            }
            catch (PermissionException pEx)
            {
                Log.Debug("Flashhhhhhhhhhhhhhhhhh", pEx.Message);
            }
            catch (Exception ex)
            {
                Log.Debug("Flashhhhhhhhhhhhhhhhhh", ex.Message);
            }
        }

        private async void Turnoff_Click(object sender, EventArgs e)
        {
            try
            {

                await Flashlight.TurnOffAsync();
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                Log.Debug("Flashhhhhhhhhhhhhhhhhh", fnsEx.Message);
            }
            catch (PermissionException pEx)
            {
                Log.Debug("Flashhhhhhhhhhlllllll", pEx.Message);
            }
            catch (Exception ex)
            {
                Log.Debug("Flashhhhhhhhhhhkkkkkkkkk", ex.Message);
            }
        }

        private void UIReference()
        {
            turnon = FindViewById<Button>(Resource.Id.button1);
            turnoff = FindViewById<Button>(Resource.Id.button2);
            take = FindViewById<Button>(Resource.Id.button3);
            image = FindViewById<ImageView>(Resource.Id.imageView1);


        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}