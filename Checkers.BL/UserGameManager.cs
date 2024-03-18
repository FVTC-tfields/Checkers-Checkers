namespace Checkers.BL
{
    public class UserGameManager : GenericManager<tblUserGame>
    {
        public List<UserGame> LoadTest()
        {
            try
            {
                List<UserGame> rows = new List<UserGame>();
                base.Load()
                    .ForEach(ug => rows.Add(
                        new UserGame
                        {
                            Id = ug.Id,
                            UserId = ug.UserId,
                            GameId = ug.GameId,
                            Color = ug.Color
                        }));

                return rows;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
