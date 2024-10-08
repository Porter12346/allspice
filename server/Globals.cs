global using System;
global using System.Data;
global using System.Collections.Generic;
global using System.IO;
global using System.Linq;

global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using System.Threading.Tasks;
global using Dapper;
global using CodeWorks.Utils;

// APPLICATION SPECIFIC
global using allspice.Repositories;
global using allspice.Services;
global using allspice.Models;
global using Amazon.Runtime;
global using Amazon.S3;
global using Amazon.S3.Model;