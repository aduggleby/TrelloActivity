namespace Chello.Core
{
	/// <summary>
	/// List of verbs todo: https://docs.google.com/spreadsheet/ccc?key=0Ar6dPH1mDdr4dFByR0FPYTAwX0dBSU9SMWMxT1Y4SlE
	/// </summary>
	public class ChelloClient
	{
		public ChelloClient()
			: this(Config.AuthKey, Config.AuthToken)
		{
		}

		public ChelloClient(string authKey)
			: this(authKey, null)
		{
		}

		public ChelloClient(string authKey, string authToken)
		{
			this.Boards = new BoardService(authKey, authToken);
			this.Lists = new ListService(authKey, authToken);
			this.Cards = new CardService(authKey, authToken);
			this.Comments = new CommentService(authKey, authToken);
			this.Members = new MemberService(authKey, authToken);
			this.Notifications = new NotificationService(authKey, authToken);
			this.Organizations = new OrganizationService(authKey, authToken);
		}

		public BoardService Boards { get; private set; }
		public ListService Lists { get; private set; }
		public CardService Cards { get; private set; }
		public CommentService Comments { get; private set; }
		public MemberService Members { get; private set; }
		public NotificationService Notifications { get; private set; }
		public OrganizationService Organizations { get; private set; }
	}
}