using System.Collections.Generic;
using System.Linq;
using theatreers.shared.Models;
using theatreers.shared.Interfaces;

public class CompanyServiceFake : ICompanyService
{
    private readonly List<Company> _CompanyList;
 
    public CompanyServiceFake()
    {
        _CompanyList = new List<Company>()
            {
                new Company() { Id = 1, Name = "Reading Amateur Operatic Society" },
                new Company() { Id = 2, Name = "EBOS" },
                new Company() { Id = 3, Name = "Sainsbury Singers" }
            };
    }
 
    public IEnumerable<Company> GetAll()
    {
        return _CompanyList;
    }
    
    public Company GetById(int id)
    {
        return _CompanyList.Where(a => a.Id == id)
            .FirstOrDefault();
    }
 
    public Company Create(Company newItem)
    {
        newItem.Id = _CompanyList.Count + 1;
        _CompanyList.Add(newItem);
        return newItem;
    }

    public Company Update(Company body)
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