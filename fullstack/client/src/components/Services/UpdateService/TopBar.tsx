import React from 'react'

export const TopBar = () => {
    return (
        <div className="border-b px-4 mb-4 mt-2 pb-4 border-[--color-text-baby-blue]">
            <div className="flex items-center justify-between p-0.5">
                <div>
                    <span className="text-sm font-bold block">
                        Update existing service
                    </span>
                    <span className="text-xs block">
                        Update service
                    </span>
                </div>
            </div>
        </div>
    )
}
