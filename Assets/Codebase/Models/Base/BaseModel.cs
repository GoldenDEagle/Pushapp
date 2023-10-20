namespace Assets.Codebase.Models.Base
{
    public abstract class BaseModel
    {
        public BaseModel()
        {
            InitModel();
        }

        /// <summary>
        /// Fills model with data.
        /// </summary>
        protected abstract void InitModel();
    }
}
