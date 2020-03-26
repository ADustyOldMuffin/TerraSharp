using System;
using System.IO;

namespace TerraSharp
{
    public static class Terraform
    {
        ///<summary>
        /// Will initialize a TerraformDirectory in the current directory
        ///</summary>
        ///<returns>A <code>TerraformDirectory</code> for the current directory </returns>
        public static TerraformDirectory Init()
        {
            return Init(Directory.GetCurrentDirectory());
        }

        public static TerraformDirectory Init(string directory = "")
        {
            if(CheckTerraformInstallation())
                throw new Exception("Terraform not installed");

            if(string.IsNullOrEmpty(directory))
                directory = Directory.GetCurrentDirectory();

            try
            {
                string result = CommandShell.RunCommand($"terraform init -input=false {directory}");

                TerraformDirectory terraformDirectory = new TerraformDirectory();
                return terraformDirectory;
            }
            catch
            {
                return null;
            }
        }

        public static bool CheckTerraformInstallation()
        {
            try
            {
                string result = CommandShell.RunCommand("terraform --version");
                if(result.ToLower().Contains("terraform"))
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
