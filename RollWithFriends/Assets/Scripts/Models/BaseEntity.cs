namespace Models
{
    public class BaseEntity
    {
        public BaseEntity()
        {

        }

        public BaseEntity(string unityCustomTokenAPI)
        {
            UnityCustomTokenAPI = unityCustomTokenAPI;
        }

        public string Id { get; set; }

        public string UnityCustomTokenAPI { get; set; }
    }
}