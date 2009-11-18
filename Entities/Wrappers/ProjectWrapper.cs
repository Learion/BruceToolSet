#region

using System;

#endregion

namespace SEOToolSet.Entities.Wrappers
{
    public class ProjectWrapper
    {
        public virtual Int32 Id { get; set; }

        public virtual string Domain { get; set; }

        public virtual string ClientName { get; set; }

        public virtual string ContactEmail { get; set; }

        public virtual string ContactName { get; set; }

        public virtual string ContactPhone { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        public virtual DateTime? UpdatedDate { get; set; }

        public virtual string CreatedBy { get; set; }

        public virtual string UpdatedBy { get; set; }

        public virtual Boolean? Enabled { get; set; }

        public virtual AccountWrapper Account { get; set; }

        public virtual string Name { get; set; }

        public static implicit operator ProjectWrapper(Project project)
        {
            if (project == null) return null;
            return new ProjectWrapper
                       {
                           Id = project.Id,
                           Domain = project.Domain,
                           ClientName = project.ClientName,
                           ContactEmail = project.ContactEmail,
                           ContactName = project.ContactName,
                           ContactPhone = project.ContactPhone,
                           CreatedDate = project.CreatedDate,
                           CreatedBy = project.CreatedBy,
                           UpdatedBy = project.UpdatedBy,
                           Enabled = project.Enabled,
                           Account = project.Account,
                           Name = project.Name
                       };
        }
    }
}