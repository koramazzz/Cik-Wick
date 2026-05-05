public class Consts
{
    public struct Scenes
    {
        public const string GAME_SCENE = "GameScene";
        public const string MENU_SCENE = "MenuScene";
    }

    public struct Layers
    {
        public const string GROUND_LAYER = "Ground";
        public const string FLOOR_LAYER = "Floor";
    }
    
    public struct PlayerAnimations
    {
        public const string IS_MOVING = "IsMoving";
        public const string IS_JUMPING = "IsJumping";
        public const string IS_SLIDING = "IsSliding";
        public const string IS_SLIDING_ACTIVE = "IsSlidingActive";
    }

    public struct CatAnimations
    {
        public const string IS_IDLING = "IsIdling";
        public const string IS_WALKING = "IsWalking";
        public const string IS_CHASING = "IsRunning";
        public const string IS_ATTACKING = "IsAttacking";
    }

    public struct WheatTypes
    {
        public const string GOLD_WHEAT = "GoldWheat";
        public const string HOLY_WHEAT = "HolyWheat";
        public const string ROTTEN_WHEAT = "RottenWheat";
    }

    public struct OtherAnimations
    {
        public const string IS_SPATULA_JUMPING = "IsSpatulaJumping";
    }
}