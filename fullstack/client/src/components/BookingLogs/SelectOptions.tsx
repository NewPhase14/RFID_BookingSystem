import { Menu, MenuButton, MenuItem, MenuItems } from '@headlessui/react'
import { ChevronDownIcon } from '@heroicons/react/20/solid'

export default function SelectOptions() {
    return (
        <Menu as="div" className="relative inline-block text-left">
            <div>
                <MenuButton className="inline-flex w-full justify-center gap-x-1.5 rounded-md bg-gray-800 px-3 py-2 text-sm font-semibold shadow-xs ring-1 ring-[--color-text-baby-blue] ring-inset hover:text-[--color-text-baby-blue] hover:bg-gray-700">
                    Options
                    <ChevronDownIcon aria-hidden="true" className="-mr-1 size-5 text-gray-400" />
                </MenuButton>
            </div>

            <MenuItems
                transition
                className="absolute right-0 z-10 mt-2 w-56 origin-top-right rounded-md bg-gray-800 shadow-lg ring-1 ring-black/5 transition focus:outline-hidden data-closed:scale-95 data-closed:transform data-closed:opacity-0 data-enter:duration-100 data-enter:ease-out data-leave:duration-75 data-leave:ease-in"
            >
                <div className="py-1">
                    <MenuItem>
                        <a
                            href="#"
                            className="block px-4 py-2 text-sm  hover:text-[--color-text-baby-blue] data-focus:outline-hidden"
                        >
                            Created bookings
                        </a>
                    </MenuItem>
                    <MenuItem>
                        <a
                            href="#"
                            className="block px-4 py-2 text-sm  hover:text-[--color-text-baby-blue] data-focus:outline-hidden"
                        >
                            Entered rooms
                        </a>
                    </MenuItem>
                    <MenuItem>
                        <a
                            href="#"
                            className="block px-4 py-2 text-sm  hover:text-[--color-text-baby-blue] data-focus:outline-hidden"
                        >
                            All
                        </a>
                    </MenuItem>
                </div>
            </MenuItems>
        </Menu>
    )
}
