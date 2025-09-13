import axios, { AxiosInstance, AxiosRequestConfig, AxiosResponse } from 'axios';
/*
TODO: Handle the Api exception to user friendly
      Support retrive the datetimeIso once you added to the service
      Suuport JWT token to secure the api
*/
export class MskManagementApi {
    private connection: AxiosInstance;
    private readonly config: AxiosRequestConfig = {
        baseURL: 'https://localhost:7230/api',
        timeout: 60000,
        headers: {
            "Cache-Control": "no-cache",
            Pragma: "no-cache",
        }
    };

    constructor()
    {
        this.connection = axios.create(this.config);
        this.connection.interceptors.request.use(
            (config) => {
                return config;
            },
            (error) => {
                return Promise.reject(error);
            }
            );
        this.connection.interceptors.response.use(
            (response: AxiosResponse) => {
                return response;
            },
            (error) => {
                console.error('Error:', error);
                return Promise.reject(error);
            }
            );
    }

    public async Get<T>(href: string, config?: AxiosRequestConfig | undefined): Promise<T> {
        const response = await this.connection.get<T>(href, config);
        return response.data;
	}

	public async Post<TData, TResult>(
		href: string,
		data?: TData,
		config?: AxiosRequestConfig | undefined
	): Promise<TResult> {
		const response = await this.connection.post<TResult>(href, data, config);
		return response.data;
	}

	public async Put<TData, TResult>(
		href: string,
		data?: TData,
		config?: AxiosRequestConfig | undefined
	): Promise<TResult> {
		const response = await this.connection.put<TResult>(href, data, config);
		return response.data;
	}
}

export const mskManagementApi = new MskManagementApi();