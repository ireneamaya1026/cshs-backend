using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSHSBackendAPI.Infrastructure.Repositories;

public class FormTemplateRepository : IFormTemplateRepository
{
    private readonly AppDbContext _context;

    public FormTemplateRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<FormTemplate>> GetAllAsync(string? formType)
    {
        var query = _context.FormTemplates
            .Where(f => f.IsActive)
            .AsQueryable();

        if (!string.IsNullOrEmpty(formType))
            query = query.Where(f => f.FormType == formType);

        return await query.ToListAsync();
    }

    public async Task<FormTemplate?> GetByFormTypeAsync(string formType) =>
        await _context.FormTemplates
            .FirstOrDefaultAsync(f => f.FormType == formType);

    public async Task<FormTemplate> CreateAsync(FormTemplate template)
    {
        _context.FormTemplates.Add(template);
        await _context.SaveChangesAsync();
        return template;
    }

    public async Task UpdateAsync(FormTemplate template)
    {
        _context.FormTemplates.Update(template);
        await _context.SaveChangesAsync();
    }
}