import { useAtom } from "jotai";
import {CreatedServiceAtom, JwtAtom} from "../../../atoms/atoms.ts";
import React, { useState } from "react";
import { availabilityClient } from "../../../apiControllerClients.ts";
import {useNavigate} from "react-router";
import {ServiceRoute} from "../../../helpers/routeConstants.tsx";
import toast from "react-hot-toast";

export const CreationView = () => {
    const weekDays: {
        dayName: string;
        dayNumber: number;
    }[] = [
        { dayName: "Monday", dayNumber: 1 },
        { dayName: "Tuesday", dayNumber: 2 },
        { dayName: "Wednesday", dayNumber: 3 },
        { dayName: "Thursday", dayNumber: 4 },
        { dayName: "Friday", dayNumber: 5 },
        { dayName: "Saturday", dayNumber: 6 },
        { dayName: "Sunday", dayNumber: 0 },
    ];

    const [createdService] = useAtom(CreatedServiceAtom);
    const [jwt] = useAtom(JwtAtom);
    const [times, setTimes] = useState<{ [key: number]: { start?: string; end?: string } }>({});
    const navigate = useNavigate();

    const handleTimeChange = (
        dayNumber: number,
        type: "start" | "end",
        value: string
    ) => {
        setTimes((prev) => ({
            ...prev,
            [dayNumber]: {
                ...prev[dayNumber],
                [type]: value,
            },
        }));
    };

    function createAvailability() {
        const dtos = Object.entries(times)
            .filter(([_, t]) => t.start && t.end)
            .map(([day, t]) => ({
                serviceId: createdService!.id,
                dayOfWeek: parseInt(day, 10),
                availableFrom: t.start! + ":00:00",
                availableTo: t.end! + ":00:00",
            }));

        if (dtos.length > 0) {
            availabilityClient.createAllAvailabilities(dtos, jwt).then(() => {
                toast.success("Availabilities created successfully");
                navigate(ServiceRoute);
            }).catch(() => {
                toast.error("Availability creation failed");
            });
        }
    }

    return (
        <div className="flex items-center justify-center">
            <div className="bg-base-100 border border-white/10 rounded-2xl p-6 max-w-lg">
                {weekDays.map((weekDay) => (
                    <div
                        key={weekDay.dayNumber}
                        className="flex items-center justify-between my-4"
                    >
                        <label className="w-24">{weekDay.dayName}</label>
                        <div className="flex items-center gap-2 flex-1">
                            <input
                                type="text"
                                placeholder="HH"
                                value={times[weekDay.dayNumber]?.start || ""}
                                onChange={(e) =>
                                    handleTimeChange(weekDay.dayNumber, "start", e.target.value)
                                }
                                className="w-14 px-4 py-3 rounded-md text-white border border-white/10 bg-textfield-grey focus:outline-white hover:border-white/30"
                            />
                            <span>-</span>
                            <input
                                type="text"
                                placeholder="HH"
                                value={times[weekDay.dayNumber]?.end || ""}
                                onChange={(e) =>
                                    handleTimeChange(weekDay.dayNumber, "end", e.target.value)
                                }
                                className="w-14 px-4 py-3 rounded-md text-white border border-white/10 bg-textfield-grey focus:outline-white hover:border-white/30"
                            />
                        </div>
                        <div className="absolute bottom-6 right-6">
                            <button
                                tabIndex={-1}
                                className="flex text-sm items-center gap-2 bg-gray-800 hover:bg-gray-700 hover:text-[--color-text-baby-blue] transition-colors rounded px-3 py-1.5"
                                onClick={createAvailability}
                            >
                                <span>Add availabilities</span>
                            </button>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
};
