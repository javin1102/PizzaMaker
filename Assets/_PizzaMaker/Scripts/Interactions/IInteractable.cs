namespace PizzaMaker
{
    public interface IInteractable
    {
        public bool IsInteractable { get; set; }
        /// <summary>
        /// Called when the object is clicked
        /// </summary>
        void OnClick(PlayerController playerController);

        /// <summary>
        /// Called when the object is hovered
        /// </summary>
        void OnHover(PlayerController playerController);

        /// <summary>
        /// Called when the object is unhovered
        /// </summary>
        void OnUnhover(PlayerController playerController);
    }
}