using Unhurd.Infrastructure.Common;

public static class PromoTasksErrors
{
    public static readonly Error NotFound = new("PromoTask.NotFound", "Promo task not found.");
    
    public static readonly Error TaskNotCreated = new("PromoTask.PromoTaskNotCreated", "Promo task not created.");
}
