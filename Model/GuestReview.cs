using Booking.App;
using BookingApp.Serializer;
using System;


namespace BookingApp.Model
{
	public class GuestReview : ISerializable
	{
		public int Id { get; set; }
		public AccommodationReservation AccommodationReservation { get; set; }
		public int Cleanliness { get; set; }
		public int RuleAdherence { get; set; }
		public string Comment { get; set; }



		public GuestReview()
			{
			}
        public GuestReview(int cleanliness, int ruleAdherence, string comment)
        {
            this.Cleanliness = cleanliness;
            this.RuleAdherence = ruleAdherence;
            this.Comment = comment;
        }
        public GuestReview(AccommodationReservation accommodationReservation, int cleanliness, int ruleAdherence, string comment)
			{
				this.AccommodationReservation = accommodationReservation;
				this.Cleanliness = cleanliness;
				this.RuleAdherence = ruleAdherence;
				this.Comment = comment;

			}

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), AccommodationReservation.Id.ToString(), Cleanliness.ToString(), RuleAdherence.ToString(), Comment };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            AccommodationReservation = new AccommodationReservation() { Id = Convert.ToInt32(values[1]) };
			Cleanliness = Convert.ToInt32(values[2]);	
			RuleAdherence = Convert.ToInt32(values[3]);
			Comment = values[4];
        }

    }

}