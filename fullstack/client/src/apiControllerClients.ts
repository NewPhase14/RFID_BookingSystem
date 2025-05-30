import {
    AuthClient,
    ServiceClient,
    AvailabilityClient,
    UserClient,
    BookingClient,
    ActivityLogsClient,
    SubscriptionClient
} from "./models/generated-client.ts";

const baseUrl = import.meta.env.VITE_API_BASE_URL
const prod = import.meta.env.PROD

export const serviceClient = new ServiceClient(prod ? "https://" + baseUrl : "http://" + baseUrl);
export const availabilityClient = new AvailabilityClient(prod ? "https://" + baseUrl : "http://" + baseUrl);
export const authClient = new AuthClient(prod ? "https://" + baseUrl : "http://" + baseUrl);
export const userClient = new UserClient(prod ? "https://" + baseUrl : "http://" + baseUrl);
export const activityLogsClient = new ActivityLogsClient(prod ? "https://" + baseUrl : "http://" + baseUrl);
export const bookingClient = new BookingClient(prod ? "https://" + baseUrl : "http://" + baseUrl);
export const subscriptionClient = new SubscriptionClient(prod ? "https://" + baseUrl : "http://" + baseUrl);