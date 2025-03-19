using tehnologiinet.Entities;

namespace tehnologiinet.Repositories;

public interface IFactorioRepository
{
    List<Production> GetAllProduction();
    List<Consumption> GetAllConsumption();
    Factorio GetProductionById(long Id);
    Factorio GetConsumptionById(long Id);
    Factorio GetProductionByItem(string item);
    Factorio GetConsumptionByItem(string item);
    void UpdateProduction(Factorio updatedProduction);
}