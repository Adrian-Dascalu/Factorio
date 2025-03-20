using tehnologiinet.Entities;

namespace tehnologiinet.Repositories;

public interface IFactorioRepository
{
    List<Production> GetAllProduction();
    List<Consumption> GetAllConsumption();
    Production GetProductionById(long Id);
    Consumption GetConsumptionById(long Id);
    Production GetProductionByItem(Items item);
    Consumption GetConsumptionByItem(Items item);
    void UpdateProduction(Factorio updatedProduction);
}