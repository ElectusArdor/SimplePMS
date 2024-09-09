using System;
using System.Linq;

namespace Simple_PMS.Models
{
    internal class ProjectTask
    {
        internal string[] Statuses { get; } = ["To do", "In progress", "Done"];

        private readonly uint projectId;
        private readonly string name;
        private readonly string description;
        private uint? ownerId;
        private string currentStatus;

        public uint ProjectId { get { return projectId; } }
        public string Name { get { return name; } }
        public string Description { get { return description; } }
        public uint? OwnerId { get { return ownerId; } set { ownerId = value; } }
        public string CurrentStatus { get { return currentStatus.ToString(); } set { currentStatus = Statuses.Contains(value) ? currentStatus = value : currentStatus = "To do"; } }

        public ProjectTask (uint projectId, string name, string description)
        {
            this.projectId = projectId;
            this.name = name;
            this.description = description;
            this.ownerId = null;
            this.currentStatus = "To do";
        }
    }
}
