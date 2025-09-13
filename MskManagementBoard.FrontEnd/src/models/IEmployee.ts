export interface IEmployee {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  fullName: string;
}
export interface IApiCreateEmployeeRequest {
  firstName: string;
  lastName: string;
  email: string;
}