using tehnologiinet.Entities;

namespace tehnologiinet.Repositories;

public interface IFactorioRepository
{
    List<Production> GetAllProduction();
    List<Consumption> GetAllConsumption();
    Production GetProductionById(long Id);
    Consumption GetConsumptionById(long Id);
    Production GetProductionByItem(Item Items);
    Consumption GetConsumptionByItem(Item Item);
    Production LoadProductionFromJson();
    void UpdateProduction(Factorio updatedProduction);
    Recipe LoadRecipesFromJson();
}