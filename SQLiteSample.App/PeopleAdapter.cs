using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace SQLiteSample.App
{
    class PeopleAdapter : RecyclerView.Adapter
    {
        private MainActivity mainActivity;

        public PeopleAdapter(MainActivity mainActivity)
        {
            this.mainActivity = mainActivity;
        }

        public override int ItemCount => mainActivity.people.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is PersonViewHolder pViewHolder)
            {
                pViewHolder.firstNameTextView.Text = mainActivity.people[position].FirstName;
                pViewHolder.lastNameTextView.Text = mainActivity.people[position].LastName;
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LinearLayout layout = (LinearLayout)LayoutInflater.From(mainActivity)
                .Inflate(Resource.Layout.layout_person, parent, false);
            PersonViewHolder viewHolder = new PersonViewHolder(layout);
            return viewHolder;
        }
    }
}