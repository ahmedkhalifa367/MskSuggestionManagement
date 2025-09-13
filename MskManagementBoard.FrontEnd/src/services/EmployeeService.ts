import { mskManagementApi, MskManagementApi } from "@/api/MskManagementApi";
import { IApiCreateEmployeeRequest, IEmployee } from "@/models/IEmployee";

export class EmployeeService {
    constructor(private mskManagementApi: MskManagementApi) {}
    public async getAllEmployees(): Promise<IEmployee[]> {
        return this.mskManagementApi.Get<IEmployee[]>('/employees');
    }

    public async addEmployee(employee: IApiCreateEmployeeRequest): Promise<IEmployee> {
        return this.mskManagementApi.Post<IApiCreateEmployeeRequest, IEmployee>('/employees', employee);
    }
}

export const employeeService = new EmployeeService(mskManagementApi);