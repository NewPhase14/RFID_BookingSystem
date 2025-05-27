import {atom} from 'jotai';
import {
    ActivityLogResponseDto, BookingResponseDto,
    ServiceResponseDto,
    UserResponseDto
} from '../models/generated-client.ts';

export const JwtAtom = atom<string>(localStorage.getItem('jwt') || '')

export const AdminAtom = atom<UserResponseDto>();

export const UsersAtom = atom<UserResponseDto[]>([]);

export const UserAtom = atom<UserResponseDto>();

export const BookingsAtom = atom<BookingResponseDto[]>([]);

export const ServicesAtom = atom<ServiceResponseDto[]>([]);

export const ServiceAtom = atom<ServiceResponseDto>();

export const LatestActivityLogsAtom = atom<ActivityLogResponseDto[]>([]);

export const LatestBookingsAtom = atom<BookingResponseDto[]>([]);

export const ActivityLogsAtom = atom<ActivityLogResponseDto[]>([]);

export const CreatedServiceAtom = atom<ServiceResponseDto>();

export const TopicAtom = atom<string>();
