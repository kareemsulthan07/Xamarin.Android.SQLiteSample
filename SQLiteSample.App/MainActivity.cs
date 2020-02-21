using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using static Android.Views.View;
using Android.Views;
using Microsoft.EntityFrameworkCore;
using Android.Support.V7.Widget;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace SQLiteSample.App
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IOnClickListener
    {
        private EditText firstNameEditText, lastNameEditText;
        private Button saveButton;
        private RecyclerView peopleRecyclerView;
        private RecyclerView.LayoutManager layoutManager;
        private RecyclerView.Adapter peopleAdapter;
        public List<Model.Person> people;
        private string dbFilePath;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            people = new List<Model.Person>();
            firstNameEditText = FindViewById<EditText>(Resource.Id.firstNameEditText);
            lastNameEditText = FindViewById<EditText>(Resource.Id.lastNameEditText);
            saveButton = FindViewById<Button>(Resource.Id.saveButton);
            peopleRecyclerView = FindViewById<RecyclerView>(Resource.Id.peopleRecyclerView);
            peopleRecyclerView.HasFixedSize = true;
            saveButton.SetOnClickListener(this);

            dbFilePath = System.IO.Path
                .Combine($"{System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal)}", "People.db");

            try
            {
                using (var db = new Model.PeopleDbContext(dbFilePath))
                {
                    await db.Database.MigrateAsync();

                    people.AddRange(await db.People.ToListAsync());

                    layoutManager = new LinearLayoutManager(this);
                    peopleRecyclerView.SetLayoutManager(layoutManager);

                    peopleAdapter = new PeopleAdapter(this);
                    peopleRecyclerView.SetAdapter(peopleAdapter);
                }
            }
            catch (System.Exception exception)
            {
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void OnClick(View v)
        {
            try
            {
                switch (v.Id)
                {
                    case Resource.Id.saveButton:
                        var person = new Model.Person()
                        {
                            FirstName = firstNameEditText.Text,
                            LastName = lastNameEditText.Text,
                        };
                        SavePerson(person);
                        break;

                    default:
                        break;
                }
            }
            catch (System.Exception exception)
            {
            }
        }

        public async void SavePerson(Model.Person person)
        {
            try
            {
                using (var db = new Model.PeopleDbContext(dbFilePath))
                {
                    db.People.Add(person);
                    var saveResult = await db.SaveChangesAsync();
                    if (saveResult > 0)
                    {
                        people.Add(person);
                        peopleAdapter.NotifyItemChanged(people.IndexOf(person));

                        Toast.MakeText(this, "Person added successfully.", ToastLength.Short).Show();
                    }
                }
            }
            catch (System.Exception exception)
            {

                throw;
            }
        }
    }
}