using System.Collections.Generic;
using System.Linq;
using theatreers.shared.Models;
using theatreers.shared.Interfaces;

public class CompanyServiceFake : ICompanyService
{
    private readonly List<CompanyModel> _CompanyList;
 
    public CompanyServiceFake()
    {
        _CompanyList = new List<CompanyModel>()
            {
                new CompanyModel() { Id = 1, Name = "Reading Amateur Operatic Society" },
                new CompanyModel() { Id = 2, Name = "EBOS" },
                new CompanyModel() { Id = 3, Name = "Sainsbury Singers" }
            };
    }
 
    public IEnumerable<CompanyModel> GetAll()
    {
        return _CompanyList;
    }
    
    public CompanyModel GetById(int id)
    {
        return _CompanyList.Where(a => a.Id == id)
            .FirstOrDefault();
    }
 
    public CompanyModel Create(CompanyModel newItem)
    {
        newItem.Id = _CompanyList.Count + 1;
        _CompanyList.Add(newItem);
        return newItem;
    }

    public CompanyModel Update(CompanyModel body)
    {
        var existing = _CompanyList.First(a => a.Id == body.Id);
        return existing;
    }
 
    public void Delete(int id)
    {
        var existing = _CompanyList.First(a => a.Id == id);
        _CompanyList.Remove(existing);
    }
}