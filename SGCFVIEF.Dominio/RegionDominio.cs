﻿using SGCFVIEF.Entidad;
using SGCFVIEF.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCFVIEF.Dominio
{
    public class RegionDominio
    {
        private RegionRepositorio oRegionRepositorio = new RegionRepositorio();

        #region "Metodos No Transaccionales"

        public List<RegionEntidad> listar()
        {
            return oRegionRepositorio.listar();
        }

        #endregion
    }
}
