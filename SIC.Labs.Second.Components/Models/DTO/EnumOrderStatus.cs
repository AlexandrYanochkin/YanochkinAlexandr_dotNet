using System;

namespace SIC.Labs.Second.Components.Models.DTO {

    [Serializable]
    public enum OrderStatus
    {
        OnProcessing,
        SentToClient,
        Finished
    }

}