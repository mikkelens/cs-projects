using DataProcessing.Types;

namespace DataProcessing.Collection;

public partial class ShowCollection
{
	private readonly List<Show> _shows;

	public ShowCollection(List<Show> shows)
	{
		_shows = shows;
	}
}