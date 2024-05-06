﻿using System.IO;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Projections;

namespace CineQuebec.Windows.BLL.Services;

public class ProjectionService : IProjectionService
{
    private readonly IProjectionRepository _projectionRepository;

    public ProjectionService(IProjectionRepository projectionRepository)
    {
        _projectionRepository = projectionRepository;
    }

    public async Task<Projection> AddProjection(Projection projection)
    {
        try
        {
            return await _projectionRepository.AddProjection(projection);
        }
        catch (Exception ex)
        {
            throw new InvalidDataException("Une erreur s'est produite lors de l'ajout de la projection : " +
                                           ex.Message);
        }
    }

    public async Task<bool> DeleteProjection(Projection projection)
    {
        try
        {
            return await _projectionRepository.DeleteProjection(projection);
        }
        catch (Exception ex)
        {
            throw new InvalidDataException("Une erreur s'est produite lors de la suppression de la projection : " +
                                           ex.Message);
        }
    }

    public async Task<List<Projection>> GetProjections(Film film)
    {
        try
        {
            return await _projectionRepository.GetProjectionsForFilm(film);
        }
        catch (Exception ex)
        {
            throw new InvalidDataException("Une erreur s'est produite lors de la récupération des projections : " +
                                           ex.Message);
        }
    }

    public async Task<bool> UpdateProjection(Projection projection)
    {
        try
        {
            return await _projectionRepository.UpdateProjection(projection);
        }
        catch (Exception ex)
        {
            throw new InvalidDataException("Une erreur s'est produite lors de la modification de la projection : " +
                                           ex.Message);
        }
    }

    public async Task<List<Salle>> GetSalles()
    {
        try
        {
            return await _projectionRepository.GetSalles();
        }
        catch (Exception ex)
        {
            throw new InvalidDataException("Une erreur s'est produite lors de la récupération des salles : " +
                                           ex.Message);
        }
    }

    public async Task<bool> estSalleDisponibleThisDay(Salle salle, DateTime day)
    {
        try
        {
            var projections = await _projectionRepository.GetProjectionsForSalle(salle, day);
            if (projections.Count == 0)
                return true;
            return false;
        }
        catch (Exception ex)
        {
            throw new InvalidDataException(
                "Une erreur s'est produite lors de la récupération des projections pour cette salle : " + ex.Message);
        }
    }
}