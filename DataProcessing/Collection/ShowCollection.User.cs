using DataProcessing.Types;

namespace DataProcessing.Collection;

public partial class ShowCollection
{
	private void RandomShowFromSpecifiedYear()
	{
		// todo: ask for number
		DisplayRandomShowFromYear(5);
	}
	private void TitlesWithSpecifiedPerson()
	{
		// todo: ask for person
		DisplayTitlesWithPerson("Steven Spielberg");
	}
	private void TitlesWithSpecificSeasonCount()
	{
		// todo: ask for count
		DisplayTitlesWithSeasonCount(5);
	}
	private void InformationOnSpecificTitle()
	{
		// todo: ask for show
		DisplayInformationOnTitle("The Matrix");
	}
	private void AverageLengthFromSpecificRating()
	{
		// todo: ask for rating
		DisplayAverageLengthFromRating("PG-13");
	}
	private void RemoveSpecificTitleFromData()
	{
		// todo: ask for title
		RemoveTitleFromDataAndDisplay("The Matrix");
	}
	private void AddSpecificShowToData()
	{
		// todo: ask for info
		Show newShow = new Show
		{
			Title = "Added show!"
		};
		AddShowToDataAndDisplay(newShow);
	}
}