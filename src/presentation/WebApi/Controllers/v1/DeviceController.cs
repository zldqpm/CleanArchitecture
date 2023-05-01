using Application.Devices.Commands.CreateDevice;
using Application.Devices.Commands.DeleteDevice;
using Application.Devices.Commands.UpdateDevice;
using Application.Devices.Queries.GetDevices;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers.v1
{
    public class DeviceController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<DevicesVm>> Get()
        {
            return await Mediator.Send(new GetDevicesQuery());
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateDeviceCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CreateDeviceCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> UpdateItemDetails(int id, UpdateDeviceCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteDeviceCommand { Id = id });

            return NoContent();
        }
    }
}
