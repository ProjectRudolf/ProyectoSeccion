using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCSeccion47.Models
{
    public partial class EmpleadoViewModels
    {
        [Key]
        public int EmpleadoID { get; set; }

        [Required (ErrorMessage ="Este Dato es Obligatrio")]
        [Display(Name = "Ficha")]
        [DataType(DataType.Text)]        
        public string Ficha { get; set; }

        [Required (ErrorMessage ="Este Dato es Obligatrio")]
        [Display(Name = "Nombre(s)")]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }

        [Required (ErrorMessage ="Este Dato es Obligatrio")]
        [Display(Name = "Apellido Paterno")]
        [DataType(DataType.Text)]
        public string ApePa { get; set; }

        [Required (ErrorMessage ="Este Dato es Obligatrio")]
        [Display(Name = "Apellido Materno")]
        [DataType(DataType.Text)]
        public string ApeMa { get; set; }

        [Required (ErrorMessage ="Este Dato es Obligatrio")]
        [Display(Name = "RFC")]
        [DataType(DataType.Text)]
        public string RFC { get; set; }

        [Required (ErrorMessage ="Este Dato es Obligatrio")]
        [Display(Name = "Situacion Contractual")]
        //[DataType(DataType.Custom)]
        public string SituacionContractual { get; set; }

        [Required (ErrorMessage ="Este Dato es Obligatrio")]
        [Display(Name = "UbicacionLaboral")]
        public string UbicacionLaboral { get; set; }

        [Display(Name = "Rol")]
        public string Rol { get; set; }

        [Required (ErrorMessage ="Este Dato es Obligatrio")]
        [Display(Name = "Region")]
        public string Region { get; set; }

        [Display(Name = "Clave Departamento")]
        [DataType(DataType.Text)]
        public string ClaveDepartamento { get; set; }

        [Required (ErrorMessage ="Este Dato es Obligatrio")]
        [Display(Name = "Nivel")]
        public int Nivel { get; set; }

        [Required (ErrorMessage ="Este Dato es Obligatrio")]
        [Display(Name = "Clave Asiste")]
        [DataType(DataType.Text)]
        public string ClaveAsiste { get; set; }

        [Display(Name = "Domicilio")]
        public string Domicilio { get; set; }

        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [Display(Name = "Ciudad")]
        public string Ciudad { get; set; }

        [Display(Name = "Telefono Casa")]
        [DataType(DataType.Text)]
        public string TelefonoCasa { get; set; }

        [Display(Name = "Telefono Celular")]
        [DataType(DataType.Text)]
        public string TelefonoCelular { get; set; }

        [Display(Name = "Correo Electronico")]
        [DataType(DataType.Text)]
        public string CorreoElectronico { get; set; }

        [Display(Name = "Nombre Banco")]
        [DataType(DataType.Text)]
        public string Banco { get; set; }

        [Display(Name = "Sucursal")]
        [DataType(DataType.Text)]
        public string Sucursal { get; set; }

        //[Required(ErrorMessage = "Este Dato es Obligatrio")]
        [Display(Name = "Número Cuenta")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} Debe Ser Solo Números")]
        //[Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        [DataType(DataType.Text)]
        public string NumeroCuenta { get; set; }

        [Display(Name = "Clave Interbancaria")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} Debe Ser Solo Números")]
        [DataType(DataType.Text)]
        public string ClaveInterbancaria { get; set; }

        [Display(Name = "Observaciones")]
        [DataType(DataType.MultilineText)]
        public string Observaciones { get; set; }

        public DateTime? FechaInserta { get; set; }
        public DateTime? FechaModifica { get; set; }
    }
}
