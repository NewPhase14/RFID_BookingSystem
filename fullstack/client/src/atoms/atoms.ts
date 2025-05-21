import {atom} from 'jotai';
import {
    ActivityLogDto, BookingResponseDto,
    ServiceResponseDto,
    UserResponseDto
} from '../models/generated-client.ts';

export const JwtAtom = atom<string>(localStorage.getItem('jwt') || '')

export const AdminAtom = atom<UserResponseDto>();

export const UsersAtom = atom<UserResponseDto[]>([]);

export const BookingsAtom = atom<BookingResponseDto[]>([]);

export const ServicesAtom = atom<ServiceResponseDto[]>([]);

export const LatestActivityLogsAtom = atom<ActivityLogDto[]>([]);

export const ActivityLogsAtom = atom<ActivityLogDto[]>([]);

export const CreatedServiceAtom = atom<ServiceResponseDto>();
