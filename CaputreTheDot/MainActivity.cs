using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;

namespace CaputreTheDot
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button btnStart;
        Button btnRules;
        Button btnDifficulty;
        Dialog chooseDifficultyDialog;
        public enum DifficultyLevel { NotSelected=0,Easy=1, Medium=2,Hard=3};
        DifficultyLevel diffLvl = DifficultyLevel.Easy;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            btnStart = FindViewById<Button>(Resource.Id.btnStart);
            btnRules = FindViewById<Button>(Resource.Id.btnRules);
            btnDifficulty = FindViewById<Button>(Resource.Id.btnDifficulty);
            btnDifficulty.Click += BtnDifficulty_Click;
            btnStart.Click += BtnStart_Click;

        }

        private void BtnStart_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(GameActivity));

            if (diffLvl == DifficultyLevel.NotSelected)         
            {
                Toast.MakeText(this, "set your difficulty", ToastLength.Long).Show();
            }
            else
            {
                intent.PutExtra("DIFFICULTY", (int)diffLvl);              
                StartActivity(intent);
            }
              

        }

        private void BtnDifficulty_Click(object sender, System.EventArgs e)
        {
            chooseDifficultyDialog = new Dialog(this);
            chooseDifficultyDialog.SetContentView(Resource.Layout.DifficultySecondryLayout);
            chooseDifficultyDialog.SetTitle("Dialog with Radio Button");
            chooseDifficultyDialog.SetCancelable(true);
            RadioButton btnEasy = chooseDifficultyDialog.FindViewById<RadioButton>(Resource.Id.rgDifficultyEasy);
            RadioButton btnMedium = chooseDifficultyDialog.FindViewById<RadioButton>(Resource.Id.rgDifficultyMedium);
            RadioButton btnHard = chooseDifficultyDialog.FindViewById<RadioButton>(Resource.Id.rgDifficultyHard);

            btnEasy.Click += RadioGroupDifficultySelected;
            btnMedium.Click += RadioGroupDifficultySelected;
            btnHard.Click += RadioGroupDifficultySelected;

            chooseDifficultyDialog.Show();            
        }

        private void RadioGroupDifficultySelected(object sender, System.EventArgs e)
        {
            //look for it
            RadioButton rbtn = (RadioButton)sender;
            System.Console.WriteLine(rbtn.Text);
            btnDifficulty.Text = "Difficulty: " + rbtn.Text;
            if (rbtn.Text == "easy") diffLvl = DifficultyLevel.Easy;
            if (rbtn.Text == "medium") diffLvl = DifficultyLevel.Medium;
            if (rbtn.Text == "hard") diffLvl = DifficultyLevel.Hard;
            chooseDifficultyDialog.Dismiss();
        }
    }
}