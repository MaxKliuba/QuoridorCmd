namespace QuoridorCmd.Model
{
    class CommandAction
    {
        public static readonly CommandAction BLACK = new CommandAction("black");
        public static readonly CommandAction WHITE = new CommandAction("white");
        public static readonly CommandAction MOVE = new CommandAction("move");
        public static readonly CommandAction JUMP = new CommandAction("jump");
        public static readonly CommandAction WALL = new CommandAction("wall");

        public string Action { get; }

        public CommandAction(string action)
        {
            Action = action.ToLower();
        }

        public override string ToString()
        {
            return Action;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                CommandAction ca = (CommandAction)obj;

                return Action.Equals(ca.Action);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
