namespace MskSuggestionManagement.Domain.Enums
{
    public enum Type
    {
        TargetedExercise,
        /// <summary>
        /// Workspace adjustments or equipment orders (e.g., changing monitor height, external mouse for laptop).
        /// TODO: Need clarification — what is the logic to differentiate between a workspace adjustment 
        /// and an equipment order? When should this enum value be used for "WorkspaceAdjustment"?
        /// </summary>
        WorkspaceAdjustment,
        BehavioralChange,
        LifestyleChange
    }
}
