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
    public class PersonViewHolder : RecyclerView.ViewHolder
    {
        public TextView firstNameTextView, lastNameTextView;

        public PersonViewHolder(View itemView) : base(itemView)
        {
            firstNameTextView = ItemView.FindViewById<TextView>(Resource.Id.fNameTextView);
            lastNameTextView = ItemView.FindViewById<TextView>(Resource.Id.lNameTextView);
        }
    }
}