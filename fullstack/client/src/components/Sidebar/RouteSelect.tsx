import React, { useState } from 'react';
import { IconType } from 'react-icons';
import { FiHome, FiUsers } from 'react-icons/fi';
import { BsFillDoorOpenFill } from 'react-icons/bs';
import { LuLogs } from 'react-icons/lu';
import "../styles.css";

const routes = [
    { Icon: FiHome, title: "Dashboard" },
    { Icon: FiUsers, title: "Users" },
    { Icon: LuLogs, title: "Logs" },
    { Icon: BsFillDoorOpenFill, title: "Rooms" },
];

export const RouteSelect = () => {
    const [selectedRoute, setSelectedRoute] = useState("Dashboard");

    return (
        <div className="space-y-1">
            {routes.map(({ Icon, title }) => (
                <Route
                    selected={selectedRoute === title}
                    Icon={Icon}
                    title={title}
                    onClick={() => setSelectedRoute(title)}
                />
            ))}
        </div>
    );
};

const Route = ({
                   selected,
                   Icon,
                   title,
                   onClick,
               }: {
    selected: boolean;
    Icon: IconType;
    title: string;
    onClick: () => void;
}) => {
    return (
        <button
            onClick={onClick}
            className={`flex items-center justify-start gap-2
                w-full rounded px-2 py-1.5 text-sm transition-[box-shadow,_background-color,_color] ${
                selected
                    ? "bg-[--color-background-grey] text-white"
                    : "hover:bg-[--color-button-grey] bg-transparent text-white/50 shadow-none"
            }`}
        >
            <Icon className={selected ? "text-text-baby-blue" : ""} />
            <span>{title}</span>
        </button>
    );
};
