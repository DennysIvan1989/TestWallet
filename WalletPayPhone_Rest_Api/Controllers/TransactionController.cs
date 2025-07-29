using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using payphone.wallet.businesslogic;
using payphone.wallet.businesslogic.Dto.Wallet;
using payphone.wallet.businesslogic.Modelos;
using payphone.wallet.businesslogic.Transacciones;
using payphone.wallet.businesslogic.Utils;
using payphone.wallet.persistence.Modelos;

namespace WalletPayPhone_Rest_Api.Controllers
{
    [Produces("application/json")]
    [Route("api/transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IWallet _wallet;
        private readonly IWalletMovement _walletMov;
        private readonly WalletDbContext _wdbcontext;

        /// <summary>
        /// Inyecci{on de dep.
        /// </summary>
        /// <param name="wallet"></param>
        /// <param name="walletMov"></param>
        /// <param name="context"></param>
        public TransactionController(IWallet wallet, IWalletMovement walletMov, WalletDbContext context)
        {
            _wallet = wallet;
            _walletMov = walletMov;
            _wdbcontext = context;
        }

        [HttpGet("wallet/{id}")]
        public IActionResult GetWalletDetails(int id){
            
            var resultado = new ResultadoDto<WalletDto>();
            try
            {

                resultado = _wallet.GetWallet(id);
            }
            catch (WalletException we)
            {

                return StatusCode(StatusCodes.Status409Conflict, new
                {

                    correcto = false,
                    codigoError = we.Code,
                    mensaje = we.Message
                });
                                
            }catch (Exception ex){

                return StatusCode(StatusCodes.Status409Conflict, new
                {

                    correcto = false,
                    codigoError = ErrorEnum.ERR999.ToString(),
                    informacion = ErrorEnum.ERR999.GetDescription(),
                    mensaje = ex.Message
                });
            }

            return Ok(resultado);
        }

        [HttpPost("wallet")]
        public IActionResult AddWallet([BindRequired, FromBody] WalletDto walletInfo)
        {

            var resultado = new ResultadoDto();
            try
            {

                resultado = _wallet.CreateWallet(walletInfo);
                _wdbcontext.SaveChanges();

            }
            catch (WalletException we)
            {

                return StatusCode(StatusCodes.Status409Conflict, new
                {

                    correcto = false,
                    codigoError = we.Code,
                    mensaje = we.Message
                });

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status409Conflict, new
                {

                    correcto = false,
                    codigoError = ErrorEnum.ERR999.ToString(),
                    informacion = ErrorEnum.ERR999.GetDescription(),
                    mensaje = ex.Message
                });
            }

            return Ok(resultado);
        }

        [HttpPut("wallet/{id}")]
        public IActionResult UpdateWallet(int id, [BindRequired, FromBody]  WalletDto wallet)
        {

            var resultado = new ResultadoDto();
            try
            {

                resultado = _wallet.UpdateWallet(id, wallet);
                _wdbcontext.SaveChanges();
            }
            catch (WalletException we)
            {

                return StatusCode(StatusCodes.Status409Conflict, new
                {

                    correcto = false,
                    codigoError = we.Code,
                    mensaje = we.Message
                });

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status409Conflict, new
                {

                    correcto = false,
                    codigoError = ErrorEnum.ERR999.ToString(),
                    informacion = ErrorEnum.ERR999.GetDescription(),
                    mensaje = ex.Message
                });
            }

            return Ok(resultado);
        }


        [HttpDelete("wallet/{id}")]
        public IActionResult DeleteWallet(int id)
        {

            var resultado = new ResultadoDto();
            try
            {

                resultado = _wallet.DeteleWallet(id);
                _wdbcontext.SaveChanges();
            }
            catch (WalletException we)
            {

                return StatusCode(StatusCodes.Status409Conflict, new
                {

                    correcto = false,
                    codigoError = we.Code,
                    mensaje = we.Message
                });

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status409Conflict, new
                {

                    correcto = false,
                    codigoError = ErrorEnum.ERR999.ToString(),
                    informacion = ErrorEnum.ERR999.GetDescription(),
                    mensaje = ex.Message
                });
            }

            return Ok(resultado);
        }

        [HttpPost("wallet-movement")]
        public IActionResult AddWalletMovement([BindRequired, FromBody] WalletMovementDto walletMov)
        {

            var resultado = new ResultadoDto();
            try
            {

                resultado = _walletMov.CreateMovement(walletMov);
                _wdbcontext.SaveChanges();
            }
            catch (WalletException we)
            {

                return StatusCode(StatusCodes.Status409Conflict, new
                {

                    correcto = false,
                    codigoError = we.Code,
                    mensaje = we.Message
                });

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status409Conflict, new
                {

                    correcto = false,
                    codigoError = ErrorEnum.ERR999.ToString(),
                    informacion = ErrorEnum.ERR999.GetDescription(),
                    mensaje = ex.Message
                });
            }

            return Ok(resultado);
        }

        [HttpGet("wallet-movement/{id}")]
        public IActionResult DetailWalletMovement(int id)
        {

            var resultado = new ResultadoDto<WalletMovementDto>();
            try
            {

                resultado = _walletMov.DetailMovement(id);
                _wdbcontext.SaveChanges();
            }
            catch (WalletException we)
            {

                return StatusCode(StatusCodes.Status409Conflict, new
                {

                    correcto = false,
                    codigoError = we.Code,
                    mensaje = we.Message
                });

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status409Conflict, new
                {

                    correcto = false,
                    codigoError = ErrorEnum.ERR999.ToString(),
                    informacion = ErrorEnum.ERR999.GetDescription(),
                    mensaje = ex.Message
                });
            }

            return Ok(resultado);
        }

        [HttpGet("wallet-movement/{dateInit}/{dateEnd}/{idWallet}")]
        public IActionResult DetailWalletMovements(DateTime dateInit, DateTime dateEnd, int idWallet)
        {

            var resultado = new ResultadoDto<List<WalletMovementDto>>();
            try
            {

                resultado = _walletMov.GetMovementRangeDate(dateInit, dateEnd, idWallet);
                _wdbcontext.SaveChanges();
            }
            catch (WalletException we)
            {

                return StatusCode(StatusCodes.Status409Conflict, new
                {

                    correcto = false,
                    codigoError = we.Code,
                    mensaje = we.Message
                });

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status409Conflict, new
                {

                    correcto = false,
                    codigoError = ErrorEnum.ERR999.ToString(),
                    informacion = ErrorEnum.ERR999.GetDescription(),
                    mensaje = ex.Message
                });
            }

            return Ok(resultado);
        }
    }
}
