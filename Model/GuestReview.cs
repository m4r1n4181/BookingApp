using System;


namespace BookingApp.Model
{
	public class GuestReview
	{
		public int GuestId { get; set; }
		private int Cleanliness { get; set; }
		private int RuleAdherence { get; set; }
		private string Comment { get; set; }



		public GuestReview()
			{
			}

		public GuestReview(int geustId, int cleanliness, int ruleAdherence, string comment)
			{
				this.GuestId = geustId;
				this.Cleanliness = cleanliness;
				this.RuleAdherence = ruleAdherence;
				this.Comment = comment;

			}

	}

}